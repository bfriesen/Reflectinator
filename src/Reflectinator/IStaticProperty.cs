using System;

namespace Reflectinator
{
    public interface IStaticProperty : IProperty
    {
        Func<object> StaticGetFunc { get; }
        Action<object> StaticSetAction { get; }

        object Get();
        void Set(object value);
    }
}