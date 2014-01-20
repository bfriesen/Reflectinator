using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public interface IMethod : IMember
    {
        MethodInfo MethodInfo { get; }
        string Name { get; }
        object Invoke(object instance, params object[] args);
        Expression<Func<object, object[], object>> InvokeExpression { get; }
        Func<object, object[], object> InvokeDelegate { get; }
    }
}