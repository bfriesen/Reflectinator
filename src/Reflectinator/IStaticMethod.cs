using System;
using System.Linq.Expressions;

namespace Reflectinator
{
    public interface IStaticMethod : IMethod
    {
        object Invoke(params object[] args);
        Expression<Func<object[], object>> InvokeExpression { get; }
        new Func<object[], object> InvokeDelegate { get; }
    }
}