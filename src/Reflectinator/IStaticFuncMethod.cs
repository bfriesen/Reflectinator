using System;

namespace Reflectinator
{
    public interface IStaticFuncMethod : IMethod
    {
        object Invoke(params object[] args);
        new Func<object[], object> InvokeDelegate { get; }
    }
}