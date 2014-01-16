using System;
using System.Reflection;

namespace Reflectinator
{
    /// <summary>
    /// Represents a constructor.
    /// </summary>
    public interface IConstructor
    {
        /// <summary>
        /// The <see cref="ConstructorInfo"/> that this instance of <see cref="IConstructor"/> represents.
        /// </summary>
        ConstructorInfo ConstructorInfo { get; }

        /// <summary>
        /// Gets a value indicating whether the constructor has public visibility.
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// Gets a <see cref="ITypeCrawler"/> that represents the constructor's declaring type.
        /// </summary>
        ITypeCrawler DeclaringType { get; }

        /// <summary>
        /// Gets an array of <see cref="ITypeCrawler"/> objects that represent the types of the constructor's parameters.
        /// </summary>
        ITypeCrawler[] Parameters { get; }

        /// <summary>
        /// Invokes the constructor, returning a new instance of the constructor's type.
        /// </summary>
        /// <param name="args">Any arguments to be passed into the constructor.</param>
        /// <returns>A new instance of the constructor's type.</returns>
        object Invoke(params object[] args);

        /// <summary>
        /// Gets a function that, when invoked, returns a new instance of the constructor's type.
        /// </summary>
        Func<object[], object> InvokeFunc { get; } 
    }
}