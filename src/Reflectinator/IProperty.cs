using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public interface IProperty : IMember
    {
        PropertyInfo PropertyInfo { get; }

        MethodInfo GetMethod { get; }
        MethodInfo SetMethod { get; }

        bool CanRead { get; }
        bool CanWrite { get; }

        ITypeCrawler PropertyType { get; }

        Expression<Func<object, object>> GetFuncExpression { get; }
        Expression<Action<object, object>> SetActionExpression { get; }

        Func<object, object> GetFunc { get; }
        Action<object, object> SetAction { get; }

        object Get(object instance);
        void Set(object instance, object value);
    }
}