using System;
using System.Reflection;

namespace Reflectinator
{
    public class CachedPropertyInfo : ICachedPropertyInfo
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly bool _isPublic;
        private readonly bool _isStatic;
        private readonly ICachedType _propertyType;
        private readonly ICachedType _declaringType;

        private readonly Lazy<Func<object, object>> _getValue;
        private readonly Lazy<Action<object, object>> _setValue;

        private readonly Lazy<Func<object>> _getValueAsStatic;
        private readonly Lazy<Action<object>> _setValueAsStatic;

        protected CachedPropertyInfo(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            _isPublic = propertyInfo.IsPublic();
            _isStatic = propertyInfo.IsStatic();
            _propertyType = CachedType.Create(propertyInfo.PropertyType);
            _declaringType = CachedType.Create(propertyInfo.DeclaringType);

            _getValue = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(propertyInfo));
            _setValue = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(propertyInfo));

            _getValueAsStatic = new Lazy<Func<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call GetAsStatic on a property that is not static.");
                }

                return () => ((ICachedPropertyInfo)this).Get(default(object));
            });
            _setValueAsStatic = new Lazy<Action<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call SetAsStatic on a property that is not static.");
                }

                return value => ((ICachedPropertyInfo)this).Set(default(object), value);
            });
        }

        public static ICachedPropertyInfo Create(PropertyInfo propertyInfo)
        {
            return new CachedPropertyInfo(propertyInfo);
        }

        public static CachedPropertyInfo<TDeclaringType, TPropertyType> Create<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return new CachedPropertyInfo<TDeclaringType, TPropertyType>(propertyInfo);
        }

        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }
        public string Name { get { return _propertyInfo.Name; } }
        public bool IsPublic { get { return _isPublic; } }
        public bool IsStatic { get { return _isStatic; } }
        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }
        public ICachedType PropertyType { get { return _propertyType; } }
        public ICachedType DeclaringType { get { return _declaringType; } }

        Func<object, object> ICachedPropertyInfo.Get { get { return _getValue.Value; } }
        Action<object, object> ICachedPropertyInfo.Set { get { return _setValue.Value; } }

        Func<object> ICachedPropertyInfo.GetAsStatic { get { return _getValueAsStatic.Value; } }
        Action<object> ICachedPropertyInfo.SetAsStatic { get { return _setValueAsStatic.Value; } }
    }
}