using System;

namespace Reflectinator
{
    public interface ITypeCrawler
    {
        Type Type { get; }
        IConstructor[] Constructors { get; }
        IField[] Fields { get; }
        IProperty[] Properties { get; }
    }
}