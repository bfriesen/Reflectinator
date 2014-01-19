using System;
using System.Reflection;

namespace Reflectinator
{
    public interface IProperty : IMember
    {
        string Name { get; }
        PropertyInfo PropertyInfo { get; }

        MethodInfo GetMethod { get; }
        MethodInfo SetMethod { get; }

        bool CanRead { get; }
        bool CanWrite { get; }

        ITypeCrawler PropertyType { get; }

        Func<object, object> GetFunc { get; }
        Action<object, object> SetAction { get; }

        object Get(object instance);
        void Set(object instance, object value);
    }
}