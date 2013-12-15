using System.Reflection;

namespace Reflectinator
{
    public abstract class CachedPropertyInfo : ICachedPropertyInfo
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly bool _isPublic;
        private readonly bool _isStatic;
        private readonly ICachedType _propertyType;
        private readonly ICachedType _declaringType;

        protected CachedPropertyInfo(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            _isPublic = (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsPublic)
                || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsPublic);
            _isStatic = (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsStatic)
                || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsStatic);
            _propertyType = CachedType.Create(propertyInfo.PropertyType);
            _declaringType = CachedType.Create(propertyInfo.DeclaringType);
        }

        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }
        public string Name { get { return _propertyInfo.Name; } }
        public bool IsPublic { get { return _isPublic; } }
        public bool IsStatic { get { return _isStatic; } }
        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }
        public ICachedType PropertyType { get { return _propertyType; } }
        public ICachedType DeclaringType { get { return _declaringType; } }

        object ICachedPropertyInfo.GetValue(object obj)
        {
            // TODO: Implement for real
            return PropertyInfo.GetValue(obj, null);
        }

        void ICachedPropertyInfo.SetValue(object obj, object value)
        {
            // TODO: Implement for real
            PropertyInfo.SetValue(obj, value, null);
        }
    }
}