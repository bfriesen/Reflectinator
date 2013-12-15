using System;
using System.Linq;
using System.Reflection;

namespace Reflectinator
{
    public class CachedType<T> : ICachedType
    {
        private readonly Type _type;
        private readonly Lazy<ICachedConstructorInfo[]> _constructors;
        private readonly Lazy<ICachedFieldInfo[]> _fields;
        private readonly Lazy<ICachedPropertyInfo[]> _properties;

        private CachedType()
        {
            _type = typeof(T);
            _constructors = new Lazy<ICachedConstructorInfo[]>(
                () =>
                _type.GetConstructors(BindingFlags.Public
                                   | BindingFlags.NonPublic
                                   | BindingFlags.Instance)
                    .Select(c =>
                    {
                        var parameters = c.GetParameters();
                        switch (parameters.Length)
                        {
                            case 0:
                                return (ICachedConstructorInfo)Activator.CreateInstance(typeof(CachedConstructorInfo<>).MakeGenericType(c.DeclaringType));
                            case 1:
                                return (ICachedConstructorInfo)Activator.CreateInstance(typeof(CachedConstructorInfo<,>).MakeGenericType(c.DeclaringType, parameters[0].ParameterType));
                            case 2:
                                return (ICachedConstructorInfo)Activator.CreateInstance(typeof(CachedConstructorInfo<,,>).MakeGenericType(c.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType));
                            default: // TODO: Implement the rest of the generic implementations of CachedConstructorInfo.
                                return null;
                        }
                    })
                    .Where(c => c != null)
                    .ToArray());
            _fields = new Lazy<ICachedFieldInfo[]>(
                () =>
                _type.GetFields(BindingFlags.Public
                                   | BindingFlags.NonPublic
                                   | BindingFlags.Instance
                                   | BindingFlags.Static)
                    .Select(f => (ICachedFieldInfo)Activator.CreateInstance(typeof(CachedFieldInfo<,>).MakeGenericType(f.DeclaringType, f.FieldType), f))
                    .ToArray());
            _properties = new Lazy<ICachedPropertyInfo[]>(
                () =>
                _type.GetProperties(BindingFlags.Public
                                   | BindingFlags.NonPublic
                                   | BindingFlags.Instance
                                   | BindingFlags.Static)
                    .Select(p => (ICachedPropertyInfo)Activator.CreateInstance(typeof(CachedPropertyInfo<,>).MakeGenericType(p.DeclaringType, p.PropertyType), p))
                    .ToArray());
        }

        public Type Type { get { return _type; } }
        public ICachedConstructorInfo[] Constructors { get { return _constructors.Value; } }
        public ICachedFieldInfo[] Fields { get { return _fields.Value; } }
        public ICachedPropertyInfo[] Properties { get { return _properties.Value; } }
    }
}