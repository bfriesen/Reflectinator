using System.Reflection;

namespace Reflectinator
{
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
}