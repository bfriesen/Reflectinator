using System.Reflection;

namespace Reflectinator
{
    public interface ICachedFieldInfo
    {
        FieldInfo FieldInfo { get; }
        string Name { get; }
        bool IsPublic { get; }
        bool IsStatic { get; }
        bool IsReadOnly { get; }
        bool IsConstant { get; }
        ICachedType FieldType { get; }
        ICachedType DeclaringType { get; }
        object Get(object obj);
        void Set(object obj, object value);
    }
}