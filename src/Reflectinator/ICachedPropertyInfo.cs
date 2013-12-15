using System.Reflection;

namespace Reflectinator
{
    public interface ICachedPropertyInfo
    {
        PropertyInfo PropertyInfo { get; }
        string Name { get; }
        bool IsPublic { get; }
        bool IsStatic { get; }
        bool CanRead { get; }
        bool CanWrite { get; }
        ICachedType PropertyType { get; }
        ICachedType DeclaringType { get; }
        object GetValue(object obj);
        void SetValue(object obj, object value);
    }
}