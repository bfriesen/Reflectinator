using System;
using System.Reflection;

namespace Reflectinator
{
    public interface IField : IMember
    {
        string Name { get; }
        FieldInfo FieldInfo { get; }

        bool IsReadOnly { get; }
        bool IsConstant { get; }

        ITypeCrawler FieldType { get; }

        Func<object, object> GetFunc { get; }
        Action<object, object> SetAction { get; }

        object Get(object obj);
        void Set(object obj, object value);
    }
}