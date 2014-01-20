using System;
using System.Linq.Expressions;

namespace Reflectinator
{
    public interface IStaticActionMethod : IStaticMethod, IActionMethod
    {
        new void Invoke(params object[] args);
        Expression<Action<object[]>> InvokeExpression { get; }
        new Action<object[]> InvokeDelegate { get; }
    }
}