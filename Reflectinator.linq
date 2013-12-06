<Query Kind="Program">
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

#define NONEST
void Main()
{
    var type = CachedType.Create<Foo>();
    var property = type.Properties.First();
    property.GetValue(new Foo { Bar = "w00t!" }).Dump();
}

public class Foo
{
    public string Bar { get; set; }
}

public interface ICachedType
{
    Type Type { get; }
    ICachedFieldInfo[] Fields { get; }
    ICachedPropertyInfo[] Properties { get; }
}

public static class CachedType
{
    private static readonly ConcurrentDictionary<Type, ICachedType> _cachedTypeCache = new ConcurrentDictionary<Type, ICachedType>();
    
    public static ICachedType Create(Type type)
    {
        return _cachedTypeCache.GetOrAdd(
            type,
            t => (ICachedType)Activator.CreateInstance(typeof(CachedType<>).MakeGenericType(t), true));
    }
    
    public static CachedType<T> Create<T>()
    {
        return (CachedType<T>)_cachedTypeCache.GetOrAdd(
            typeof(T),
            t => (ICachedType)Activator.CreateInstance(typeof(CachedType<T>), true));
    }
}

public class CachedType<T> : ICachedType
{
    private readonly Type _type;
    private readonly Lazy<ICachedFieldInfo[]> _fields;
    private readonly Lazy<ICachedPropertyInfo[]> _properties;

    private CachedType()
    {
        _type = typeof(T);
        _fields = new Lazy<ICachedFieldInfo[]>(
            () =>
            _type.GetFields(BindingFlags.Public
                               | BindingFlags.NonPublic
                               | BindingFlags.Instance
                               | BindingFlags.Static)
                .Select(f => (ICachedFieldInfo)Activator.CreateInstance(typeof(CachedFieldInfo<,>).MakeGenericType(f.DeclaringType, f.FieldType), f))
                .ToArray());
        _properties = new Lazy<ICachedPropertyInfo[]>(
            () =>
            _type.GetProperties(BindingFlags.Public
                               | BindingFlags.NonPublic
                               | BindingFlags.Instance
                               | BindingFlags.Static)
                .Select(p => (ICachedPropertyInfo)Activator.CreateInstance(typeof(CachedPropertyInfo<,>).MakeGenericType(p.DeclaringType, p.PropertyType), p))
                .ToArray());
    }
    
    public Type Type { get { return _type; } }
    public ICachedFieldInfo[] Fields { get { return _fields.Value; } }
    public ICachedPropertyInfo[] Properties { get { return _properties.Value; } }
}

#region CachedFieldInfo

public interface ICachedFieldInfo
{
    FieldInfo FieldInfo { get; }
    ICachedType FieldType { get; }
    ICachedType DeclaringType { get; }
    object GetValue(object obj);
    void SetValue(object obj, object value);
}

public class CachedFieldInfo<TDeclaringType, TFieldType> : ICachedFieldInfo
{
    private readonly FieldInfo _fieldInfo;
    private readonly ICachedType _fieldType;
    private readonly ICachedType _declaringType;

    public CachedFieldInfo(FieldInfo fieldInfo)
    {
        _fieldInfo = fieldInfo;
        _fieldType = CachedType.Create(fieldInfo.FieldType);
        _declaringType = CachedType.Create(fieldInfo.DeclaringType);
    }
    
    public FieldInfo FieldInfo { get { return _fieldInfo; } }
    public ICachedType FieldType { get { return _fieldType; } }
    public ICachedType DeclaringType { get { return _declaringType; } }
    
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
    ICachedType PropertyType { get; }
    ICachedType DeclaringType { get; }
    object GetValue(object obj);
    void SetValue(object obj, object value);
}

public abstract class CachedPropertyInfo : ICachedPropertyInfo
{
    private readonly PropertyInfo _propertyInfo;
    private readonly ICachedType _propertyType;
    private readonly ICachedType _declaringType;

    protected CachedPropertyInfo(PropertyInfo propertyInfo)
    {
        _propertyInfo = propertyInfo;
        _propertyType = CachedType.Create(propertyInfo.PropertyType);
        _declaringType = CachedType.Create(propertyInfo.DeclaringType);
    }
    
    public PropertyInfo PropertyInfo { get { return _propertyInfo; } }
    public ICachedType PropertyType { get { return _propertyType; } }
    public ICachedType DeclaringType { get { return _declaringType; } }
    
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