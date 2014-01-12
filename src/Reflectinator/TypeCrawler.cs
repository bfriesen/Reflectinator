using System;
using System.Collections.Concurrent;

namespace Reflectinator
{
    public static class TypeCrawler
    {
        private static readonly ConcurrentDictionary<Type, Func<ITypeCrawler>> _createTypeCrawlerMap = new ConcurrentDictionary<Type, Func<ITypeCrawler>>();

        public static ITypeCrawler Create(Type type)
        {
            var createTypeCrawler = _createTypeCrawlerMap.GetOrAdd(
                type,
                t => FuncFactory.CreateDefaultConstructorFunc<ITypeCrawler>(typeof(TypeCrawler<>).MakeGenericType(t).GetConstructorInfo()));
            return createTypeCrawler();
        }

        public static TypeCrawler<T> Create<T>()
        {
            var createTypeCrawler = _createTypeCrawlerMap.GetOrAdd(
                typeof(T),
                t => FuncFactory.CreateDefaultConstructorFunc<ITypeCrawler>(typeof(TypeCrawler<T>).GetConstructorInfo()));
            return (TypeCrawler<T>)createTypeCrawler();
        }
    }
}