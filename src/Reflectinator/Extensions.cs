using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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

        public static IEnumerable<IStaticProperty> OfStatic(this IEnumerable<IProperty> properties)
        {
            return properties.OfType<IStaticProperty>();
        }

        public static IEnumerable<IProperty> OfInstance(this IEnumerable<IProperty> properties)
        {
            return properties.Where(p => !p.IsStatic);
        }

        public static IEnumerable<IProperty> OfPublic(this IEnumerable<IProperty> properties)
        {
            return properties.Where(p => p.IsPublic);
        }

        public static IEnumerable<IStaticProperty> OfPublic(this IEnumerable<IStaticProperty> properties)
        {
            return properties.Where(p => p.IsPublic);
        }

        public static IEnumerable<IProperty> OfNonPublic(this IEnumerable<IProperty> properties)
        {
            return properties.Where(p => !p.IsPublic);
        }

        public static IEnumerable<IStaticProperty> OfNonPublic(this IEnumerable<IStaticProperty> properties)
        {
            return properties.Where(p => !p.IsPublic);
        }

        public static IEnumerable<IStaticField> OfStatic(this IEnumerable<IField> fields)
        {
            return fields.OfType<IStaticField>();
        }

        public static IEnumerable<IField> OfInstance(this IEnumerable<IField> fields)
        {
            return fields.Where(f => !f.IsStatic);
        }

        public static IEnumerable<IField> OfPublic(this IEnumerable<IField> fields)
        {
            return fields.Where(p => p.IsPublic);
        }

        public static IEnumerable<IStaticField> OfPublic(this IEnumerable<IStaticField> fields)
        {
            return fields.Where(p => p.IsPublic);
        }

        public static IEnumerable<IField> OfNonPublic(this IEnumerable<IField> fields)
        {
            return fields.Where(p => !p.IsPublic);
        }

        public static IEnumerable<IStaticField> OfNonPublic(this IEnumerable<IStaticField> fields)
        {
            return fields.Where(p => !p.IsPublic);
        }

        public static IEnumerable<IActionMethod> OfAction(this IEnumerable<IMethod> methods)
        {
            return methods.OfType<IActionMethod>();
        }

        public static IEnumerable<IStaticActionMethod> OfAction(this IEnumerable<IStaticMethod> methods)
        {
            return methods.OfType<IStaticActionMethod>();
        }

        public static IEnumerable<TMethod> OfFunc<TMethod>(this IEnumerable<TMethod> methods)
            where TMethod : IMethod
        {
            return methods.Where(m => !(m is IActionMethod));
        }

        public static IEnumerable<IStaticMethod> OfStatic(this IEnumerable<IMethod> methods)
        {
            return methods.OfType<IStaticMethod>();
        }

        public static IEnumerable<IStaticActionMethod> OfStatic(this IEnumerable<IActionMethod> methods)
        {
            return methods.OfType<IStaticActionMethod>();
        }

        public static IEnumerable<TMethod> OfInstance<TMethod>(this IEnumerable<TMethod> methods)
            where TMethod : IMethod
        {
            return methods.Where(m => !m.IsStatic);
        }

        public static IEnumerable<TMethod> OfPublic<TMethod>(this IEnumerable<TMethod> methods)
            where TMethod : IMethod
        {
            return methods.Where(m => m.IsPublic);
        }

        public static IEnumerable<TMethod> OfNonPublic<TMethod>(this IEnumerable<TMethod> methods)
            where TMethod : IMethod
        {
            return methods.Where(m => !m.IsPublic);
        }

        public static IEnumerable<TMethod> ExceptPropertyAccessors<TMethod>(this IEnumerable<TMethod> methods)
            where TMethod : IMethod
        {
            return methods.Where(m => !m.MethodInfo.IsPropertyAccessor());
        }

        public static IEnumerable<TMethod> OnlyPropertyAccessors<TMethod>(this IEnumerable<TMethod> methods)
            where TMethod : IMethod
        {
            return methods.Where(m => m.MethodInfo.IsPropertyAccessor());
        }

        private static bool IsPropertyAccessor(this MethodInfo methodInfo)
        {
            return (methodInfo.Name.StartsWith("get_") || methodInfo.Name.StartsWith("set_")) && methodInfo.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length > 0;
        }

        internal static IEnumerable<T> Then<T>(this T instance, IEnumerable<T> others)
        {
            return new[] { instance }.Concat(others);
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
            return GetAggregatedHashCode(collection.ConcatNonGeneric(additionalItems));
        }

        public static int GetAggregatedHashCode(this IEnumerable collection)
        {
            return collection.AggregateNonGeneric(0, GetNextHashCode);
        }

        private static int GetNextHashCode(int currentHashCode, object nextItem)
        {
            unchecked
            {
                return (currentHashCode * 397) ^ nextItem.GetHashCode();
            }
        }

        private static IEnumerable ConcatNonGeneric(this IEnumerable collection1, IEnumerable collection2)
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

        private static TResult AggregateNonGeneric<TResult>(this IEnumerable collection, TResult seed, Func<TResult, object, TResult> func)
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