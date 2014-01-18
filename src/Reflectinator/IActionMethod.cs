using System;

namespace Reflectinator
{
    public interface IActionMethod : IMethod
    {
        new void Invoke(object instance, params object[] args);
        new Action<object, object[]> InvokeDelegate { get; } 
    }
}