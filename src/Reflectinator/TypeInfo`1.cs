using System;
using System.Linq;
using System.Reflection;

namespace Reflectinator
{
    public class TypeInfo<T> : ITypeInfo
    {
        private readonly Type _type;
        private readonly Lazy<IConstructor[]> _constructors;
        private readonly Lazy<IField[]> _fields;
        private readonly Lazy<IProperty[]> _properties;

        private TypeInfo()
        {
            _type = typeof(T);
            _constructors = new Lazy<IConstructor[]>(
                () =>
                _type.GetConstructors(
                        BindingFlags.Public
                        | BindingFlags.NonPublic
                        | BindingFlags.Instance)
                    .Select(c =>
                    {
                        var parameters = c.GetParameters();
                        switch (parameters.Length)
                        {
                            case 0:
                                return (IConstructor)Activator.CreateInstance(typeof(Constructor<>).MakeGenericType(c.DeclaringType));
                            case 1:
                                return (IConstructor)Activator.CreateInstance(typeof(Constructor<,>).MakeGenericType(c.DeclaringType, parameters[0].ParameterType));
                            case 2:
                                return (IConstructor)Activator.CreateInstance(typeof(Constructor<,,>).MakeGenericType(c.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType));
                            default: // TODO: Implement the rest of the generic implementations of Constructor.
                                return null;
                        }
                    })
                    .Where(c => c != null)
                    .ToArray());
            _fields = new Lazy<IField[]>(
                () =>
                _type.GetFields(
                        BindingFlags.Public
                        | BindingFlags.NonPublic
                        | BindingFlags.Instance
                        | BindingFlags.Static)
                    .Select(f => (IField)Activator.CreateInstance(typeof(Field<,>).MakeGenericType(f.DeclaringType, f.FieldType), f))
                    .ToArray());
            _properties = new Lazy<IProperty[]>(
                () =>
                _type.GetProperties(
                        BindingFlags.Public
                        | BindingFlags.NonPublic
                        | BindingFlags.Instance
                        | BindingFlags.Static)
                    .Select(p => (IProperty)Activator.CreateInstance(typeof(Property<,>).MakeGenericType(p.DeclaringType, p.PropertyType), p))
                    .ToArray());
        }

        public Type Type { get { return _type; } }
        public IConstructor[] Constructors { get { return _constructors.Value; } }
        public IField[] Fields { get { return _fields.Value; } }
        public IProperty[] Properties { get { return _properties.Value; } }
    }
}