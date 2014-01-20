namespace Reflectinator
{
    public interface IMember
    {
        /// <summary>
        /// Gets a <see cref="ITypeCrawler"/> that represents the constructor's declaring type.
        /// </summary>
        ITypeCrawler DeclaringType { get; }

        string Name { get; }
        bool IsPublic { get; }
        bool IsStatic { get; } 
    }
}