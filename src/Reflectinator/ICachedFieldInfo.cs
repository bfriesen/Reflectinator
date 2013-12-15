using System.Reflection;

namespace Reflectinator
{
    public interface ICachedFieldInfo
    {
        FieldInfo FieldInfo { get; }
        ICachedType FieldType { get; }
        ICachedType DeclaringType { get; }
        object GetValue(object obj);
        void SetValue(object obj, object value);
    }
}