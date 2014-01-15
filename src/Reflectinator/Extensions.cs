using System;
using System.Linq;
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

        public static ConstructorInfo GetConstructorInfo(this Type type, Type[] parameterTypes)
        {
            var ctor = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, parameterTypes, null);

            if (ctor == null)
            {
                throw new InvalidOperationException(string.Format("No constructor exists for type '{0}' with parameters of type '[{1}]'", type.Name, string.Join(", ", parameterTypes.Select(t => t.Name))));
            }

            return ctor;
        }
    }
}