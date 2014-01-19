using System;
using System.Reflection;

namespace Reflectinator
{
    public interface IMethod : IMember
    {
        MethodInfo MethodInfo { get; }
        string Name { get; }
        object Invoke(object instance, params object[] args);
        Func<object, object[], object> InvokeDelegate { get; }
    }
}