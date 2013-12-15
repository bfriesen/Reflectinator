using System.Dynamic;
using System.Reflection;

namespace Reflectinator
{
    public interface ICachedConstructorInfo : IDynamicMetaObjectProvider
    {
        ConstructorInfo ConstructorInfo { get; }
        bool IsPublic { get; }
        ICachedType DeclaringType { get; }
        ICachedType[] Parameters { get; }
        object Invoke(params object[] args);
    }
}