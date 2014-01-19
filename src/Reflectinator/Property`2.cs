using System;
using System.Reflection;

namespace Reflectinator
{
    public class Property<TDeclaringType, TPropertyType> : Member, IProperty
    {
        private readonly PropertyInfo _propertyInfo;

        private readonly Lazy<MethodInfo> _getMethod;
        private readonly Lazy<MethodInfo> _setMethod;

        private readonly Lazy<bool> _isPublic;

        private readonly Lazy<ITypeCrawler> _propertyType;

        private readonly Lazy<Func<object, object>> _getValueLooselyTyped;
        private readonly Lazy<Action<object, object>> _setValueLooselyTyped;

        private readonly Lazy<Func<TDeclaringType, TPropertyType>> _getValueStronglyTyped;
        private readonly Lazy<Action<TDeclaringType, TPropertyType>> _setValueStronglyTyped;

        internal Property(PropertyInfo propertyInfo)
            : base(propertyInfo)
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

            _propertyType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(propertyInfo.PropertyType));

            _getValueLooselyTyped = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(propertyInfo));
            _setValueLooselyTyped = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(propertyInfo));

            _getValueStronglyTyped = new Lazy<Func<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateGetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));
            _setValueStronglyTyped = new Lazy<Action<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateSetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));
        }

        public string Name { get { return _propertyInfo.Name; } }
        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }

        public MethodInfo GetMethod { get { return _getMethod.Value; } }
        public MethodInfo SetMethod { get { return _setMethod.Value; } }

        public override bool IsPublic { get { return _isPublic.Value; } }
        public override bool IsStatic { get { return false; } }

        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }

        public ITypeCrawler PropertyType { get { return _propertyType.Value; } }

        Func<object, object> IProperty.GetFunc { get { return _getValueLooselyTyped.Value; } }
        Action<object, object> IProperty.SetAction { get { return _setValueLooselyTyped.Value; } }

        object IProperty.Get(object instance) { return _getValueLooselyTyped.Value(instance); }
        void IProperty.Set(object instance, object value) { _setValueLooselyTyped.Value(instance, value); }

        public Func<TDeclaringType, TPropertyType> GetFunc { get { return _getValueStronglyTyped.Value; } }
        public Action<TDeclaringType, TPropertyType> SetAction { get { return _setValueStronglyTyped.Value; } }

        public TPropertyType Get(TDeclaringType instance) { return _getValueStronglyTyped.Value(instance); }
        public void Set(TDeclaringType instance, TPropertyType value) { _setValueStronglyTyped.Value(instance, value); }
    }
}