using System;

namespace Reflectinator
{
    public interface IStaticField : IField
    {
        Func<object> StaticGetFunc { get; }
        Action<object> StaticSetAction { get; }

        object Get();
        void Set(object value);
    }
}