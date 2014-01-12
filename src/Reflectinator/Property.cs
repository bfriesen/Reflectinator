using System;
using System.Reflection;

namespace Reflectinator
{
    public class Property : IProperty
    {
        private readonly PropertyInfo _propertyInfo;

        private readonly Lazy<MethodInfo> _getMethod;
        private readonly Lazy<MethodInfo> _setMethod;

        private readonly Lazy<bool> _isPublic;
        private readonly Lazy<bool> _isStatic;

        private readonly Lazy<ITypeInfo> _propertyType;
        private readonly Lazy<ITypeInfo> _declaringType;

        private readonly Lazy<Func<object, object>> _getValue;
        private readonly Lazy<Action<object, object>> _setValue;

        private readonly Lazy<Func<object>> _getValueAsStatic;
        private readonly Lazy<Action<object>> _setValueAsStatic;

        protected Property(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;

            _getMethod = new Lazy<MethodInfo>(() => propertyInfo.GetGetMethod(true));
            _setMethod = new Lazy<MethodInfo>(() => propertyInfo.GetSetMethod(true));

            _isPublic = new Lazy<bool>(propertyInfo.IsPublic);
            _isStatic = new Lazy<bool>(propertyInfo.IsStatic);

            _propertyType = new Lazy<ITypeInfo>(() => TypeInfo.Create(propertyInfo.PropertyType));
            _declaringType = new Lazy<ITypeInfo>(() => TypeInfo.Create(propertyInfo.DeclaringType));

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
        public static IProperty Create(PropertyInfo propertyInfo)
        {
            return new Property(propertyInfo);
        }

        public static Property<TDeclaringType, TPropertyType> Create<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return new Property<TDeclaringType, TPropertyType>(propertyInfo);
        }

        public string Name { get { return _propertyInfo.Name; } }
        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }

        public MethodInfo GetMethod { get { return _getMethod.Value; } }
        public MethodInfo SetMethod { get { return _setMethod.Value; } }

        public bool IsPublic { get { return _isPublic.Value; } }
        public bool IsStatic { get { return _isStatic.Value; } }

        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }

        public ITypeInfo PropertyType { get { return _propertyType.Value; } }
        public ITypeInfo DeclaringType { get { return _declaringType.Value; } }

        object IProperty.Get(object instance) { return _getValue.Value(instance); }
        void IProperty.Set(object instance, object value) { _setValue.Value(instance, value); }

        object IProperty.Get() { return _getValueAsStatic.Value(); }
        void IProperty.Set(object value) { _setValueAsStatic.Value(value); }
    }
}