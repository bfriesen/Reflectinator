<Query Kind="Program">
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

#define NONEST
void Main()
{
    var type = CachedType.Create(typeof(Foo));
    var property = type.Properties.OfType<CachedPropertyInfo<Foo, string>>().First();
    property.GetValue(new Foo { Bar = "w00t!" }).Dump();
}

public class Foo
{
    public string Bar { get; set; }
}

public abstract class CachedType
{
    private static readonly ConcurrentDictionary<Type, CachedType> _cachedTypeCache = new ConcurrentDictionary<Type, CachedType>();

    private readonly Type _type;
    private readonly Lazy<ICachedFieldInfo[]> _fields;
    private readonly Lazy<ICachedPropertyInfo[]> _properties;

    protected CachedType(Type type)
    {
        _type = type;
        _fields = new Lazy<ICachedFieldInfo[]>(
            () =>
            type.GetFields(BindingFlags.Public
                               | BindingFlags.NonPublic
                               | BindingFlags.Instance
                               | BindingFlags.Static)
                .Select(f => (CachedFieldInfo)Activator.CreateInstance(typeof(CachedFieldInfo<,>).MakeGenericType(f.DeclaringType, f.FieldType), f))
                .ToArray());
        _properties = new Lazy<ICachedPropertyInfo[]>(
            () =>
            type.GetProperties(BindingFlags.Public
                               | BindingFlags.NonPublic
                               | BindingFlags.Instance
                               | BindingFlags.Static)
                .Select(p => (CachedPropertyInfo)Activator.CreateInstance(typeof(CachedPropertyInfo<,>).MakeGenericType(p.DeclaringType, p.PropertyType), p))
                .ToArray());
    }
    
    public static CachedType Create(Type type)
    {
        return _cachedTypeCache.GetOrAdd(
            type,
            t => (CachedType)Activator.CreateInstance(typeof(CachedType<>).MakeGenericType(t), true));
    }
    
    public static CachedType<T> Create<T>()
    {
        return (CachedType<T>)_cachedTypeCache.GetOrAdd(
            typeof(T),
            t => (CachedType)Activator.CreateInstance(typeof(CachedType<T>), true));
    }
    
    public Type Type { get { return _type; } }
    public ICachedFieldInfo[] Fields { get { return _fields.Value; } }
    public ICachedPropertyInfo[] Properties { get { return _properties.Value; } }
}

public class CachedType<T> : CachedType
{
    private CachedType()
        : base(typeof(T))
    {        
    }
}

#region CachedFieldInfo

public interface ICachedFieldInfo
{
    FieldInfo FieldInfo { get; }
    CachedType FieldType { get; }
    CachedType DeclaringType { get; }
    object GetValue(object obj);
    void SetValue(object obj, object value);
}

public abstract class CachedFieldInfo : ICachedFieldInfo
{
    private readonly FieldInfo _fieldInfo;
    private readonly CachedType _fieldType;
    private readonly CachedType _declaringType;

    public CachedFieldInfo(FieldInfo fieldInfo)
    {
        _fieldInfo = fieldInfo;
        _fieldType = CachedType.Create(fieldInfo.FieldType);
        _declaringType = CachedType.Create(fieldInfo.DeclaringType);
    }
    
    public FieldInfo FieldInfo { get { return _fieldInfo; } }
    public CachedType FieldType { get { return _fieldType; } }
    public CachedType DeclaringType { get { return _declaringType; } }
    
    object ICachedFieldInfo.GetValue(object obj)
    {
        // TODO: Implement for real
        return FieldInfo.GetValue(obj);
    }
    
    void ICachedFieldInfo.SetValue(object obj, object value)
    {
        // TODO: Implement for real
        FieldInfo.SetValue(obj, value);
    }
}

public class CachedFieldInfo<TDeclaringType, TFieldType> : CachedFieldInfo
{
    public CachedFieldInfo(FieldInfo fieldInfo)
        : base(fieldInfo)
    {
    }
    
    public TFieldType GetValue(TDeclaringType obj)
    {
        return (TFieldType)((ICachedPropertyInfo)this).GetValue(obj);
    }
    
    public void SetValue(TDeclaringType obj, TFieldType value)
    {
        ((ICachedPropertyInfo)this).SetValue(obj, value);
    }
}

#endregion

#region CachedPropertyInfo

public interface ICachedPropertyInfo
{
    PropertyInfo PropertyInfo { get; }
    CachedType PropertyType { get; }
    CachedType DeclaringType { get; }
    object GetValue(object obj);
    void SetValue(object obj, object value);
}

public abstract class CachedPropertyInfo : ICachedPropertyInfo
{
    private readonly PropertyInfo _propertyInfo;
    private readonly CachedType _propertyType;
    private readonly CachedType _declaringType;

    protected CachedPropertyInfo(PropertyInfo propertyInfo)
    {
        _propertyInfo = propertyInfo;
        _propertyType = CachedType.Create(propertyInfo.PropertyType);
        _declaringType = CachedType.Create(propertyInfo.DeclaringType);
    }
    
    public PropertyInfo PropertyInfo { get { return _propertyInfo; } }
    public CachedType PropertyType { get { return _propertyType; } }
    public CachedType DeclaringType { get { return _declaringType; } }
    
    object ICachedPropertyInfo.GetValue(object obj)
    {
        // TODO: Implement for real
        return PropertyInfo.GetValue(obj);
    }
    
    void ICachedPropertyInfo.SetValue(object obj, object value)
    {
        // TODO: Implement for real
        PropertyInfo.SetValue(obj, value);
    }
}

public class CachedPropertyInfo<TDeclaringType, TPropertyType> : CachedPropertyInfo
{
    public CachedPropertyInfo(PropertyInfo propertyInfo)
        : base(propertyInfo)
    {
    }
    
    public TPropertyType GetValue(TDeclaringType obj)
    {
        return (TPropertyType)((ICachedPropertyInfo)this).GetValue(obj);
    }
    
    public void SetValue(TDeclaringType obj, TPropertyType value)
    {
        ((ICachedPropertyInfo)this).SetValue(obj, value);
    }
}

#endregion

#region CachedMethodInfo

public interface ICachedMethodInfo
{
    MethodInfo MethodInfo { get; }
    CachedType DeclaringType { get; }
    
    // DeclaringType, Parameters, Invoke, ReturnType
}

#endregion