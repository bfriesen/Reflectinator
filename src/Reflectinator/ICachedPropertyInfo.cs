using System.Reflection;

namespace Reflectinator
{
    public interface ICachedPropertyInfo
    {
        PropertyInfo PropertyInfo { get; }
        bool IsPublic { get; }
        bool IsStatic { get; }
        ICachedType PropertyType { get; }
        ICachedType DeclaringType { get; }
        object GetValue(object obj);
        void SetValue(object obj, object value);
    }
}