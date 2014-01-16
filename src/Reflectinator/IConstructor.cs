using System;
using System.Reflection;

namespace Reflectinator
{
    public interface IConstructor
    {
        ConstructorInfo ConstructorInfo { get; }
        bool IsPublic { get; }
        ITypeCrawler DeclaringType { get; }
        ITypeCrawler[] Parameters { get; }
        object Invoke(params object[] args);
        Func<object[], object> InvokeFunc { get; } 
    }
}