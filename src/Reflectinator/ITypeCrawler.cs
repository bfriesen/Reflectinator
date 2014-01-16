using System;

namespace Reflectinator
{
    /// <summary>
    /// Provides fast, cached access to the members of an arbitrary <see cref="System.Type"/>.
    /// </summary>
    public interface ITypeCrawler
    {
        /// <summary>
        /// Gets the type whose members are accessed by this instance of <see cref="ITypeCrawler"/>.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets a collection of <see cref="IConstructor"/> objects that represent the constructors of <see cref="Type"/>.
        /// </summary>
        IConstructor[] Constructors { get; }

        /// <summary>
        /// Gets a collection of <see cref="IField"/> objects that represent the fields of <see cref="Type"/>.
        /// </summary>
        IField[] Fields { get; }

        /// <summary>
        /// Gets a collection of <see cref="IProperty"/> objects that represent the properties of <see cref="Type"/>.
        /// </summary>
        IProperty[] Properties { get; }
    }
}