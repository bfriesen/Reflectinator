using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectinator
{
    public class Property : IProperty
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type, string>, IProperty> _propertiesMap = new ConcurrentDictionary<Tuple<Type, Type, Type, string>, IProperty>();

        private readonly PropertyInfo _propertyInfo;

        private readonly Lazy<MethodInfo> _getMethod;
        private readonly Lazy<MethodInfo> _setMethod;

        private readonly Lazy<bool> _isPublic;
        private readonly Lazy<bool> _isStatic;

        private readonly Lazy<ITypeCrawler> _propertyType;
        private readonly Lazy<ITypeCrawler> _declaringType;

        private readonly Lazy<Func<object, object>> _getValue;
        private readonly Lazy<Action<object, object>> _setValue;

        private readonly Lazy<Func<object>> _getValueAsStatic;
        private readonly Lazy<Action<object>> _setValueAsStatic;

        internal Property(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;

            _getMethod = new Lazy<MethodInfo>(() => propertyInfo.GetGetMethod(true));
            _setMethod = new Lazy<MethodInfo>(() => propertyInfo.GetSetMethod(true));

            _isPublic = new Lazy<bool>(propertyInfo.IsPublic);
            _isStatic = new Lazy<bool>(propertyInfo.IsStatic);

            _propertyType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(propertyInfo.PropertyType));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(propertyInfo.DeclaringType));

            _getValue = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(propertyInfo));
            _setValue = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(propertyInfo));

            _getValueAsStatic = new Lazy<Func<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a property that is not static.");
                }

                var iThis = (IProperty)this;
                return () => iThis.Get(null);
            });
            _setValueAsStatic = new Lazy<Action<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(object value) on a property that is not static.");
                }

                var iThis = (IProperty)this;
                return value => iThis.Set(null, value);
            });
        }

        // NOTE: We're returning the interface because the Get and Set properties are implemented explicitly.
        //       If we didn't do this, then the object returned wouldn't have a visible Get or Set method. And
        //       that wouldn't be a very nice API, now would it?
        public static IProperty Get(PropertyInfo propertyInfo)
        {
            return _propertiesMap.GetOrAdd(
                Tuple.Create(propertyInfo.DeclaringType, propertyInfo.PropertyType, propertyInfo.DeclaringType, propertyInfo.Name),
                t => (IProperty)Activator.CreateInstance(typeof(Property<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { propertyInfo }, null));
        }

        public static Property<TDeclaringType, TPropertyType> Get<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return (Property<TDeclaringType, TPropertyType>)_propertiesMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TPropertyType), propertyInfo.DeclaringType, propertyInfo.Name),
                t => new Property<TDeclaringType, TPropertyType>(propertyInfo));
        }

        public string Name { get { return _propertyInfo.Name; } }
        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }

        public MethodInfo GetMethod { get { return _getMethod.Value; } }
        public MethodInfo SetMethod { get { return _setMethod.Value; } }

        public bool IsPublic { get { return _isPublic.Value; } }
        public bool IsStatic { get { return _isStatic.Value; } }

        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }

        public ITypeCrawler PropertyType { get { return _propertyType.Value; } }
        public ITypeCrawler DeclaringType { get { return _declaringType.Value; } }

        Func<object, object> IProperty.GetFunc { get { return _getValue.Value; } }
        Action<object, object> IProperty.SetAction { get { return _setValue.Value; } }

        Func<object> IProperty.GetStaticFunc { get { return _getValueAsStatic.Value; } }
        Action<object> IProperty.SetStaticAction { get { return _setValueAsStatic.Value; } }

        object IProperty.Get(object instance) { return _getValue.Value(instance); }
        void IProperty.Set(object instance, object value) { _setValue.Value(instance, value); }

        object IProperty.Get() { return _getValueAsStatic.Value(); }
        void IProperty.Set(object value) { _setValueAsStatic.Value(value); }
    }
}