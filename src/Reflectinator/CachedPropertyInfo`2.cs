using System;
using System.Reflection;

namespace Reflectinator
{
    public class CachedPropertyInfo<TDeclaringType, TPropertyType> : CachedPropertyInfo
    {
        private readonly Lazy<Func<TDeclaringType, TPropertyType>> _getValue;
        private readonly Lazy<Action<TDeclaringType, TPropertyType>> _setValue;

        private readonly Lazy<Func<TPropertyType>> _getValueAsStatic;
        private readonly Lazy<Action<TPropertyType>> _setValueAsStatic;

        public CachedPropertyInfo(PropertyInfo propertyInfo)
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

            _getValue = new Lazy<Func<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateGetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));
            _setValue = new Lazy<Action<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateSetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));

            _getValueAsStatic = new Lazy<Func<TPropertyType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call GetAsStatic on a property that is not static.");
                }

                return () => Get(default(TDeclaringType));
            });
            _setValueAsStatic = new Lazy<Action<TPropertyType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call SetAsStatic on a property that is not static.");
                }

                return value => Set(default(TDeclaringType), value);
            });
        }

        public Func<TDeclaringType, TPropertyType> Get { get { return _getValue.Value; } }
        public Action<TDeclaringType, TPropertyType> Set { get { return _setValue.Value; } }

        public Func<TPropertyType> GetAsStatic { get { return _getValueAsStatic.Value; } }
        public Action<TPropertyType> SetAsStatic { get { return _setValueAsStatic.Value; } } 
    }
}