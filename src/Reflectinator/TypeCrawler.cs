using System;
using System.Collections.Concurrent;

namespace Reflectinator
{
    /// <summary>
    /// Provides static methods for accessing instances of <see cref="ITypeCrawler"/> or <see cref="TypeCrawler{T}"/>.
    /// </summary>
    public static class TypeCrawler
    {
        private static readonly ConcurrentDictionary<Type, ITypeCrawler> _typeCrawlerMap = new ConcurrentDictionary<Type, ITypeCrawler>();

        /// <summary>
        /// Get an instance of <see cref="ITypeCrawler"/> that accesses the members of <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> whose members are accessed by the return value.</param> 
        /// <returns>An instance of <see cref="ITypeCrawler"/> that accesses the members of <paramref name="type"/>.</returns>
        public static ITypeCrawler Get(Type type)
        {
            return _typeCrawlerMap.GetOrAdd(
                type,
                t => (ITypeCrawler)Activator.CreateInstance(typeof(TypeCrawler<>).MakeGenericType(t), true));
        }

        /// <summary>
        /// Gets an instance of <see cref="TypeCrawler{T}"/> that accesses memebers of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="System.Type"/> whose members are accessed by the return value.</typeparam>
        /// <returns>An instance of <see cref="TypeCrawler{T}"/> that accesses the members of <typeparamref name="T"/>.</returns>
        public static TypeCrawler<T> Get<T>()
        {
            return (TypeCrawler<T>)_typeCrawlerMap.GetOrAdd(
                typeof(T),
                _ => new TypeCrawler<T>());
        }
    }
}