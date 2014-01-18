using System;
using System.Collections;
using System.Collections.Generic;
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

        public static IEnumerable<IStaticProperty> AsStatic(this IEnumerable<IProperty> properties)
        {
            return properties.OfType<IStaticProperty>();
        }

        public static IEnumerable<IProperty> AsInstance(this IEnumerable<IProperty> properties)
        {
            return properties.Where(p => !p.IsStatic);
        }

        internal static int GetCacheKey(this Type declaringType, params Type[] argTypes)
        {
            return argTypes.GetAggregatedHashCode(declaringType);
        }

        internal static int GetCacheKey(this MethodInfo methodInfo, params Type[] argTypes)
        {
            return argTypes.GetAggregatedHashCode(methodInfo.Name, methodInfo.DeclaringType);
        }

        public static int GetAggregatedHashCode(this IEnumerable collection, params object[] additionalItems)
        {
            return GetAggregatedHashCode(collection.Concat(additionalItems));
        }

        public static int GetAggregatedHashCode(this IEnumerable collection)
        {
            return collection.Aggregate(0, GetNextHashCode);
        }

        private static int GetNextHashCode(int currentHashCode, object nextItem)
        {
            unchecked
            {
                return (currentHashCode * 397) ^ nextItem.GetHashCode();
            }
        }

        private static IEnumerable Concat(this IEnumerable collection1, IEnumerable collection2)
        {
            foreach (var item in collection1)
            {
                yield return item;
            }

            foreach (var item in collection2)
            {
                yield return item;
            }
        }

        private static TResult Aggregate<TResult>(this IEnumerable collection, TResult seed, Func<TResult, object, TResult> func)
        {
            TResult current = seed;

            foreach (var item in collection)
            {
                current = func(current, item);
            }

            return current;
        }
    }
}