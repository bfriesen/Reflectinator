using System;
using System.Linq.Expressions;

namespace Reflectinator
{
    public interface IStaticField : IField
    {
        Expression<Func<object>> StaticGetFuncExpression { get; }
        Expression<Action<object>> StaticSetActionExpression { get; }

        Func<object> StaticGetFunc { get; }
        Action<object> StaticSetAction { get; }

        object Get();
        void Set(object value);
    }
}