using System;

namespace Reflectinator
{
    public interface ITypeInfo
    {
        Type Type { get; }
        IConstructor[] Constructors { get; }
        IField[] Fields { get; }
        IProperty[] Properties { get; }
    }
}