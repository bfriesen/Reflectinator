using System.Reflection;

namespace Reflectinator
{
    public interface IField
    {
        string Name { get; }
        FieldInfo FieldInfo { get; }

        bool IsPublic { get; }
        bool IsStatic { get; }

        bool IsReadOnly { get; }
        bool IsConstant { get; }

        ITypeCrawler FieldType { get; }
        ITypeCrawler DeclaringType { get; }

        object Get(object obj);
        void Set(object obj, object value);

        object Get();
        void Set(object value);
    }
}