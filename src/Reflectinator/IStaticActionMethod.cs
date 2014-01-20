using System;

namespace Reflectinator
{
    public interface IStaticActionMethod : IStaticMethod, IActionMethod
    {
        new void Invoke(params object[] args);
        new Action<object[]> InvokeDelegate { get; }
    }
}