using System;
using System.Reflection;

namespace Reflectinator
{
    public interface ICachedPropertyInfo
    {
        string Name { get; }
        PropertyInfo PropertyInfo { get; }

        MethodInfo GetMethod { get; }
        MethodInfo SetMethod { get; }

        bool IsPublic { get; }
        bool IsStatic { get; }

        bool CanRead { get; }
        bool CanWrite { get; }

        ICachedType PropertyType { get; }
        ICachedType DeclaringType { get; }

        Func<object, object> Get { get; }
        Action<object, object> Set { get; }

        Func<object> GetAsStatic { get; }
        Action<object> SetAsStatic { get; }
    }
}