using System;

namespace Reflectinator
{
    public interface ICachedType
    {
        Type Type { get; }
        ICachedConstructorInfo[] Constructors { get; }
        ICachedFieldInfo[] Fields { get; }
        ICachedPropertyInfo[] Properties { get; }
    }
}