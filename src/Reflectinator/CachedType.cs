using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Reflectinator
{
    public static class CachedType
    {
        private static readonly ConcurrentDictionary<Type, Func<ICachedType>> _createCachedTypeMap = new ConcurrentDictionary<Type, Func<ICachedType>>();

        public static ICachedType Create(Type type)
        {
            var createCachedType = _createCachedTypeMap.GetOrAdd(
                type,
                t => FuncFactory.CreateDefaultConstructorFunc<ICachedType>(typeof(CachedType<>).MakeGenericType(t).GetCachedTypeConstructor()));
            return createCachedType();
        }

        public static CachedType<T> Create<T>()
        {
            var createCachedType = _createCachedTypeMap.GetOrAdd(
                typeof(T),
                t => FuncFactory.CreateDefaultConstructorFunc<ICachedType>(typeof(CachedType<T>).GetCachedTypeConstructor()));
            return (CachedType<T>)createCachedType();
        }

        private static ConstructorInfo GetCachedTypeConstructor(this Type t)
        {
            var ctor = t.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            Debug.Assert(ctor != null, "CachedType<T> must have a parameterless constructor.");
            return ctor;
        }
    }
}