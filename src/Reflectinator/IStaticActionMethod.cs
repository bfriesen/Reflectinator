using System;

namespace Reflectinator
{
    public interface IStaticActionMethod : IActionMethod
    {
        void Invoke(params object[] args);
        new Action<object[]> InvokeDelegate { get; }
    }
}