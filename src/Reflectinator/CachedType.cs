using System;
using System.Collections.Concurrent;

namespace Reflectinator
{
    public static class CachedType
    {
        private static readonly ConcurrentDictionary<Type, ICachedType> _cachedTypeCache = new ConcurrentDictionary<Type, ICachedType>();

        public static ICachedType Create(Type type)
        {
            return _cachedTypeCache.GetOrAdd(
                type,
                t => (ICachedType)Activator.CreateInstance(typeof(CachedType<>).MakeGenericType(t), true));
        }

        public static CachedType<T> Create<T>()
        {
            return (CachedType<T>)_cachedTypeCache.GetOrAdd(
                typeof(T),
                t => (ICachedType)Activator.CreateInstance(typeof(CachedType<T>), true));
        }
    }
}