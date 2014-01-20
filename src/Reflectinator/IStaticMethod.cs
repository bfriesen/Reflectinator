using System;

namespace Reflectinator
{
    public interface IStaticMethod : IMethod
    {
        object Invoke(params object[] args);
        new Func<object[], object> InvokeDelegate { get; }
    }
}