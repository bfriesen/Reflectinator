using System;
using System.Reflection;

namespace Reflectinator
{
    public interface IMethod
    {
        MethodInfo MethodInfo { get; }
        string Name { get; }
        bool IsPublic { get; }
        bool IsStatic { get; }
        object Invoke(object instance, params object[] args);
        Func<object, object[], object> InvokeDelegate { get; }
    }
}