<Query Kind="Program">
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

#define NONEST
void Main()
{
    var type = CachedType.Create<Foo>();
    var ctor = type.Constructors.First();
    var property = type.Properties.First();
    LINQPad.Extensions.Dump(property.GetValue(ctor.Invoke("a")));

//    var ctor1 = new CachedConstructorInfo<string, int, Foo>();
//    Foo foo1 = ctor1.Invoke("a", 1);
//    foo1.Dump();
//
//    dynamic ctor2 = new CachedConstructorInfo<string, int, Foo>();
//    Foo foo2 = ctor2("b", 2);
//    foo2.Dump();
    
//    var ctor = (Func<object[], object>)FuncFactory.CreateConstructorFunc(typeof(Foo).GetConstructor(new[] { typeof(string) }), false);
//    Foo foo = (Foo)ctor(new object[] { "w00t!!!!!!" });
//    foo.Dump();
}

public class Foo
{
//    public Foo()
//    {
//    }
    
    public Foo(string bar)
    {
        Bar = bar;
    }
    
//    public Foo(string bar, int baz)
//    {
//        Bar = bar;
//        Baz = baz;
//    }
    
    public string Bar { get; set; }
    public int Baz { get; set; }
}

#region CachedType

public interface ICachedType
{
    Type Type { get; }
    ICachedConstructorInfo[] Constructors { get; }
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
    private readonly Lazy<ICachedConstructorInfo[]> _constructors;
    private readonly Lazy<ICachedFieldInfo[]> _fields;
    private readonly Lazy<ICachedPropertyInfo[]> _properties;

    private CachedType()
    {
        _type = typeof(T);
        _constructors = new Lazy<ICachedConstructorInfo[]>(
            () =>
            _type.GetConstructors(BindingFlags.Public
                               | BindingFlags.NonPublic
                               | BindingFlags.Instance)
                .Select(c =>
                {
                    var parameters = c.GetParameters();
                    switch (parameters.Length)
                    {
                        case 0:
                            return (ICachedConstructorInfo)Activator.CreateInstance(typeof(CachedConstructorInfo<>).MakeGenericType(c.DeclaringType));
                        case 1:
                            return (ICachedConstructorInfo)Activator.CreateInstance(typeof(CachedConstructorInfo<,>).MakeGenericType(parameters[0].ParameterType, c.DeclaringType));
                        case 2:
                            return (ICachedConstructorInfo)Activator.CreateInstance(typeof(CachedConstructorInfo<,,>).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, c.DeclaringType));
                        default: // TODO: Implement the rest of the generic implementations of CachedConstructorInfo.
                            return null;
                    }
                })
                .Where(c => c != null)
                .ToArray());
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
    public ICachedConstructorInfo[] Constructors { get { return _constructors.Value; } }
    public ICachedFieldInfo[] Fields { get { return _fields.Value; } }
    public ICachedPropertyInfo[] Properties { get { return _properties.Value; } }
}

#endregion

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

#region CachedConstructorInfo

public interface ICachedConstructorInfo : IDynamicMetaObjectProvider
{
    ConstructorInfo ConstructorInfo { get; }
    ICachedType DeclaringType { get; }
    ICachedType[] Parameters { get; }
    object Invoke(params object[] args);
}

public abstract class CachedConstructorInfo : DynamicObject, ICachedConstructorInfo
{
    private readonly Func<object[], object> _invoke;
    private readonly ConstructorInfo _constructorInfo;
    private readonly ICachedType _declaringType;
    private readonly ICachedType[] _parameters;

    protected CachedConstructorInfo(ConstructorInfo constructorInfo)
    {
        _invoke = (Func<object[], object>)FuncFactory.CreateConstructorFunc(constructorInfo, false);
        _constructorInfo = constructorInfo;
        _declaringType = CachedType.Create(constructorInfo.DeclaringType);
        _parameters = constructorInfo.GetParameters().Select(p => CachedType.Create(p.ParameterType)).ToArray();
    }
    
    public ConstructorInfo ConstructorInfo { get { return _constructorInfo; } }
    public ICachedType DeclaringType { get { return _declaringType; } }
    public ICachedType[] Parameters { get { return _parameters; } }
    
    public object Invoke(params object[] args)
    {
        return _invoke(args);
    }
}

public class CachedConstructorInfo<TDeclaringType> : CachedConstructorInfo
{
    private readonly Func<TDeclaringType> _invoke;

    public CachedConstructorInfo()
        : base(typeof(TDeclaringType).GetConstructor(Type.EmptyTypes))
    {
        _invoke = (Func<TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true);
    }
    
    public TDeclaringType Invoke()
    {
        return _invoke();
    }
    
    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
    {
        result = null;
        
        if (args.Length != 0)
        {
            return false;
        }
    
        try
        {
            result = Invoke();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class CachedConstructorInfo<TArg1, TDeclaringType> : CachedConstructorInfo
{
    private readonly Func<TArg1, TDeclaringType> _invoke;

    public CachedConstructorInfo()
        : base(typeof(TDeclaringType).GetConstructor(new [] { typeof(TArg1) }))
    {
        _invoke = (Func<TArg1, TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true);
    }
    
    public TDeclaringType Invoke(TArg1 arg1)
    {
        return _invoke(arg1);
    }
    
    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
    {
        result = null;
        
        if (args.Length != 1)
        {
            return false;
        }
    
        try
        {
            result = Invoke((TArg1)args[0]);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class CachedConstructorInfo<TArg1, TArg2, TDeclaringType> : CachedConstructorInfo
{
    private readonly Func<TArg1, TArg2, TDeclaringType> _invoke;

    public CachedConstructorInfo()
        : base(typeof(TDeclaringType).GetConstructor(new [] { typeof(TArg1), typeof(TArg2) }))
    {
        _invoke = (Func<TArg1, TArg2, TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true);
    }
    
    public TDeclaringType Invoke(TArg1 arg1, TArg2 arg2)
    {
        return _invoke(arg1, arg2);
    }
    
    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
    {
        result = null;
        
        if (args.Length != 2)
        {
            return false;
        }
    
        try
        {
            result = Invoke((TArg1)args[0], (TArg2)args[1]);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

#endregion

#region FuncFactory

public static class FuncFactory
{
    public static Delegate CreateConstructorFunc(ConstructorInfo ctor, bool stronglyTyped)
    {
        var expression = CreateConstructorExpression(ctor, stronglyTyped);
        return expression.Compile();
    }
    
    public static LambdaExpression CreateConstructorExpression(ConstructorInfo ctor, bool stronglyTyped)
    {
        if (ctor == null)
        {
            throw new ArgumentNullException("ctor");
        }
    
        NewExpression expressionBody;
        ParameterExpression[] parameters;
        Type returnType;
        
        if (stronglyTyped)
        {
            parameters = ctor.GetParameters().Select(p => Expression.Parameter(p.ParameterType)).ToArray();
            expressionBody = Expression.New(ctor, parameters);
            returnType = expressionBody.Type;
        }
        else
        {
            parameters = new[] { Expression.Parameter(typeof(object[])) };
            
            var ctorArgs =
                ctor.GetParameters().Select((ctorParameter, index) => new { ctorParameter, lambdaParameter = Expression.ArrayAccess(parameters[0], Expression.Constant(index)) })
                .Select(x => 
                    x.ctorParameter.ParameterType.IsValueType
                        ? Expression.Convert(x.lambdaParameter, x.ctorParameter.ParameterType)
                        : Expression.TypeAs(x.lambdaParameter, x.ctorParameter.ParameterType)).ToArray();
            expressionBody = Expression.New(ctor, ctorArgs);
            
            returnType = typeof(object);
        }
        
        return GetLambdaExpressionForFunc(expressionBody, parameters, returnType);
    }

    private static LambdaExpression GetLambdaExpressionForFunc(Expression expression, ParameterExpression[] parameters, Type returnType)
    {
        switch (parameters.Length)
        {
            case 0:
                return
                    Expression.Lambda(
                        typeof(Func<>).MakeGenericType(
                            returnType),
                        expression);
            case 1:
                return
                    Expression.Lambda(
                        typeof(Func<,>).MakeGenericType(
                            parameters[0].Type,
                            returnType),
                        expression,
                        parameters);
            case 2:
                return
                    Expression.Lambda(
                        typeof(Func<,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            returnType),
                        expression,
                        parameters);
            case 3:
                return
                    Expression.Lambda(
                        typeof(Func<,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            returnType),
                        expression,
                        parameters);
            case 4:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            returnType),
                        expression,
                        parameters);
            case 5:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            returnType),
                        expression,
                        parameters);
            case 6:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            returnType),
                        expression,
                        parameters);
            case 7:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            returnType),
                        expression,
                        parameters);
            case 8:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            returnType),
                        expression,
                        parameters);
            case 9:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            returnType),
                        expression,
                        parameters);
            case 10:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            returnType),
                        expression,
                        parameters);
            case 11:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            parameters[10].Type,
                            returnType),
                        expression,
                        parameters);
            case 12:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            parameters[10].Type,
                            parameters[11].Type,
                            returnType),
                        expression,
                        parameters);
            case 13:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            parameters[10].Type,
                            parameters[11].Type,
                            parameters[12].Type,
                            returnType),
                        expression,
                        parameters);
            case 14:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            parameters[10].Type,
                            parameters[11].Type,
                            parameters[12].Type,
                            parameters[13].Type,
                            returnType),
                        expression,
                        parameters);
            case 15:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            parameters[10].Type,
                            parameters[11].Type,
                            parameters[12].Type,
                            parameters[13].Type,
                            parameters[14].Type,
                            returnType),
                        expression,
                        parameters);
            case 16:
                return
                    Expression.Lambda(
                        typeof(Func<,,,,,,,,,,,,,,,,>).MakeGenericType(
                            parameters[0].Type,
                            parameters[1].Type,
                            parameters[2].Type,
                            parameters[3].Type,
                            parameters[4].Type,
                            parameters[5].Type,
                            parameters[6].Type,
                            parameters[7].Type,
                            parameters[8].Type,
                            parameters[9].Type,
                            parameters[10].Type,
                            parameters[11].Type,
                            parameters[12].Type,
                            parameters[13].Type,
                            parameters[14].Type,
                            parameters[15].Type,
                            returnType),
                        expression,
                        parameters);
            default:
                throw new InvalidOperationException("Too many constructor parameters.");
        }
    }
}

#endregion