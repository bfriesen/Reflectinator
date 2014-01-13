using System;
using System.Collections.Concurrent;

namespace Reflectinator
{
    public static class TypeCrawler
    {
        private static readonly ConcurrentDictionary<Type, ITypeCrawler> _typeCrawlerMap = new ConcurrentDictionary<Type, ITypeCrawler>();

        public static ITypeCrawler Get(Type type)
        {
            return _typeCrawlerMap.GetOrAdd(
                type,
                t => (ITypeCrawler)Activator.CreateInstance(typeof(TypeCrawler<>).MakeGenericType(t), true));
        }

        public static TypeCrawler<T> Get<T>()
        {
            return (TypeCrawler<T>)_typeCrawlerMap.GetOrAdd(
                typeof(T),
                _ => (ITypeCrawler)Activator.CreateInstance(typeof(TypeCrawler<T>), true));
        }
    }
}