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

        private readonly Lazy<Func<object, object>> _getValueFunc;
        private readonly Lazy<Action<object, object>> _setValueFunc;

        public CachedPropertyInfo(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            _isPublic = (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsPublic)
                || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsPublic);
            _isStatic = (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsStatic)
                || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsStatic);
            _propertyType = CachedType.Create(propertyInfo.PropertyType);
            _declaringType = CachedType.Create(propertyInfo.DeclaringType);

            _getValueFunc = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(propertyInfo));
            _setValueFunc = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(propertyInfo));
        }

        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }
        public string Name { get { return _propertyInfo.Name; } }
        public bool IsPublic { get { return _isPublic; } }
        public bool IsStatic { get { return _isStatic; } }
        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }
        public ICachedType PropertyType { get { return _propertyType; } }
        public ICachedType DeclaringType { get { return _declaringType; } }

        public Func<object, object> Get { get { return _getValueFunc.Value; } }
        public Action<object, object> Set { get { return _setValueFunc.Value; } }
    }
}