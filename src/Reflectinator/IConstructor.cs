using System.Dynamic;
using System.Reflection;

namespace Reflectinator
{
    public interface IConstructor : IDynamicMetaObjectProvider
    {
        ConstructorInfo ConstructorInfo { get; }
        bool IsPublic { get; }
        ITypeCrawler DeclaringType { get; }
        ITypeCrawler[] Parameters { get; }
        object Invoke(params object[] args);
    }
}