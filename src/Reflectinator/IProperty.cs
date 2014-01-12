using System.Reflection;

namespace Reflectinator
{
    public interface IProperty
    {
        string Name { get; }
        PropertyInfo PropertyInfo { get; }

        MethodInfo GetMethod { get; }
        MethodInfo SetMethod { get; }

        bool IsPublic { get; }
        bool IsStatic { get; }

        bool CanRead { get; }
        bool CanWrite { get; }

        ITypeInfo PropertyType { get; }
        ITypeInfo DeclaringType { get; }

        object Get(object instance);
        void Set(object instance, object value);

        object Get();
        void Set(object value);
    }
}