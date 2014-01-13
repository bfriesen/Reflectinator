using System;
using System.Reflection;

namespace Reflectinator
{
    public sealed class Property<TDeclaringType, TPropertyType> : IProperty
    {
        private readonly PropertyInfo _propertyInfo;

        private readonly Lazy<MethodInfo> _getMethod;
        private readonly Lazy<MethodInfo> _setMethod;

        private readonly Lazy<bool> _isPublic;
        private readonly Lazy<bool> _isStatic;

        private readonly Lazy<ITypeCrawler> _propertyType;
        private readonly Lazy<ITypeCrawler> _declaringType;

        private readonly Lazy<Func<object, object>> _getValueLooselyTyped;
        private readonly Lazy<Action<object, object>> _setValueLooselyTyped;

        private readonly Lazy<Func<object>> _getValueAsStaticLooselyTyped;
        private readonly Lazy<Action<object>> _setValueAsStaticLooselyTyped;

        private readonly Lazy<Func<TDeclaringType, TPropertyType>> _getValueStronglyTyped;
        private readonly Lazy<Action<TDeclaringType, TPropertyType>> _setValueStronglyTyped;

        private readonly Lazy<Func<TPropertyType>> _getValueAsStaticStronglyTyped;
        private readonly Lazy<Action<TPropertyType>> _setValueAsStaticStronglyTyped;

        internal Property(PropertyInfo propertyInfo)
        {
            if (!typeof(TDeclaringType).IsAssignableFrom(propertyInfo.DeclaringType))
            {
                throw new ArgumentException("TDeclaringType is not assignable from the DeclaringType of the propertyInfo", "propertyInfo");
            }

            if (!typeof(TPropertyType).IsAssignableFrom(propertyInfo.PropertyType))
            {
                throw new ArgumentException("TPropertyType is not assignable from the PropertyType of the propertyInfo", "propertyInfo");
            }

            _propertyInfo = propertyInfo;

            _getMethod = new Lazy<MethodInfo>(() => propertyInfo.GetGetMethod(true));
            _setMethod = new Lazy<MethodInfo>(() => propertyInfo.GetSetMethod(true));

            _isPublic = new Lazy<bool>(propertyInfo.IsPublic);
            _isStatic = new Lazy<bool>(propertyInfo.IsStatic);

            _propertyType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(propertyInfo.PropertyType));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(propertyInfo.DeclaringType));

            _getValueLooselyTyped = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(propertyInfo));
            _setValueLooselyTyped = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(propertyInfo));

            _getValueAsStaticLooselyTyped = new Lazy<Func<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a property that is not static.");
                }

                var iThis = (IProperty)this;
                return () => iThis.Get(null);
            });
            _setValueAsStaticLooselyTyped = new Lazy<Action<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(object value) on a property that is not static.");
                }

                var iThis = (IProperty)this;
                return value => iThis.Set(null, value);
            });

            _getValueStronglyTyped = new Lazy<Func<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateGetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));
            _setValueStronglyTyped = new Lazy<Action<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateSetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));

            _getValueAsStaticStronglyTyped = new Lazy<Func<TPropertyType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a property that is not static.");
                }

                return () => Get(default(TDeclaringType));
            });
            _setValueAsStaticStronglyTyped = new Lazy<Action<TPropertyType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(TPropertyValue value) on a property that is not static.");
                }

                return value => Set(default(TDeclaringType), value);
            });
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

        Func<object, object> IProperty.GetFunc { get { return _getValueLooselyTyped.Value; } }
        Action<object, object> IProperty.SetAction { get { return _setValueLooselyTyped.Value; } }

        Func<object> IProperty.GetStaticFunc { get { return _getValueAsStaticLooselyTyped.Value; } }
        Action<object> IProperty.SetStaticAction { get { return _setValueAsStaticLooselyTyped.Value; } }

        object IProperty.Get(object instance) { return _getValueLooselyTyped.Value(instance); }
        void IProperty.Set(object instance, object value) { _setValueLooselyTyped.Value(instance, value); }

        object IProperty.Get() { return _getValueAsStaticLooselyTyped.Value(); }
        void IProperty.Set(object value) { _setValueAsStaticLooselyTyped.Value(value); }

        public Func<TDeclaringType, TPropertyType> GetFunc { get { return _getValueStronglyTyped.Value; } }
        public Action<TDeclaringType, TPropertyType> SetAction { get { return _setValueStronglyTyped.Value; } }

        public Func<TPropertyType> GetStaticFunc { get { return _getValueAsStaticStronglyTyped.Value; } }
        public Action<TPropertyType> SetStaticAction { get { return _setValueAsStaticStronglyTyped.Value; } }

        public TPropertyType Get(TDeclaringType instance) { return _getValueStronglyTyped.Value(instance); }
        public void Set(TDeclaringType instance, TPropertyType value) { _setValueStronglyTyped.Value(instance, value); }

        public TPropertyType Get() { return _getValueAsStaticStronglyTyped.Value(); }
        public void Set(TPropertyType value) { _setValueAsStaticStronglyTyped.Value(value); }
    }
}