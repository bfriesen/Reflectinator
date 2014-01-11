using System.Reflection;

namespace Reflectinator
{
    public static class Extensions
    {
        public static bool IsPublic(this PropertyInfo propertyInfo)
        {
            return (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsPublic)
                   || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsPublic);
        }

        public static bool IsStatic(this PropertyInfo propertyInfo)
        {
            return (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsStatic)
                   || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsStatic);
        }
    }
}