using System.Reflection;

namespace Reflectinator
{
    public interface ICachedFieldInfo
    {
        string Name { get; }
        FieldInfo FieldInfo { get; }

        bool IsPublic { get; }
        bool IsStatic { get; }

        bool IsReadOnly { get; }
        bool IsConstant { get; }

        ICachedType FieldType { get; }
        ICachedType DeclaringType { get; }

        object Get(object obj);
        void Set(object obj, object value);

        //object Get(); // TODO: implement static fields (and test them!)
        //void Set(object value);
    }
}