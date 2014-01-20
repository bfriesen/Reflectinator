using System;
using System.Linq.Expressions;

namespace Reflectinator
{
    public interface IActionMethod : IMethod
    {
        new void Invoke(object instance, params object[] args);
        Expression<Action<object, object[]>> InvokeExpression { get; }
        new Action<object, object[]> InvokeDelegate { get; }
    }
}