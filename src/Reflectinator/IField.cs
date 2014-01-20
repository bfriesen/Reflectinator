using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public interface IField : IMember
    {
        FieldInfo FieldInfo { get; }

        bool IsReadOnly { get; }
        bool IsConstant { get; }

        ITypeCrawler FieldType { get; }

        Expression<Func<object, object>> GetFuncExpression { get; }
        Expression<Action<object, object>> SetActionExpression { get; }

        Func<object, object> GetFunc { get; }
        Action<object, object> SetAction { get; }

        object Get(object obj);
        void Set(object obj, object value);
    }
}