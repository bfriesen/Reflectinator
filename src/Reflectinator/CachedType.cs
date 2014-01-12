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
                t => FuncFactory.CreateDefaultConstructorFunc<ICachedType>(typeof(CachedType<>).MakeGenericType(t).GetConstructorInfo()));
            return createCachedType();
        }

        public static CachedType<T> Create<T>()
        {
            var createCachedType = _createCachedTypeMap.GetOrAdd(
                typeof(T),
                t => FuncFactory.CreateDefaultConstructorFunc<ICachedType>(typeof(CachedType<T>).GetConstructorInfo()));
            return (CachedType<T>)createCachedType();
        }
    }
}