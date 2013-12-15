using System.Reflection;

namespace Reflectinator
{
    public class CachedPropertyInfo<TDeclaringType, TPropertyType> : CachedPropertyInfo
    {
        public CachedPropertyInfo(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        public TPropertyType Get(TDeclaringType obj)
        {
            return (TPropertyType)((ICachedPropertyInfo)this).GetValue(obj);
        }

        public void Set(TDeclaringType obj, TPropertyType value)
        {
            ((ICachedPropertyInfo)this).SetValue(obj, value);
        }
    }
}