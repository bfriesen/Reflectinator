using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflectinator
{
    /// <summary>
    /// Provides fast, cached access to the members of an arbitrary <see cref="System.Type"/>, <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="System.Type"/> that this instance of <see cref="TypeCrawler{T}"/> provides access to.</typeparam>
    public sealed class TypeCrawler<T> : ITypeCrawler
    {
        private readonly Type _type;
        private readonly Lazy<IConstructor[]> _constructors;
        private readonly Lazy<IField[]> _fields;
        private readonly Lazy<IProperty[]> _properties;

        private readonly Lazy<IDictionary<string, IField>> _fieldMap;
        private readonly Lazy<IDictionary<string, IProperty>> _propertyMap;

        internal TypeCrawler()
        {
            _type = typeof(T);

            _constructors = new Lazy<IConstructor[]>(
                () =>
                _type.GetConstructors(
                        BindingFlags.Public
                        | BindingFlags.NonPublic
                        | BindingFlags.Instance)
                    .Where(c => c.GetParameters().All(p => !p.ParameterType.IsPointer))
                    .Select(Constructor.Get)
                    .ToArray());

            _fields = new Lazy<IField[]>(
                () =>
                _type.GetFields(
                        BindingFlags.Public
                        | BindingFlags.NonPublic
                        | BindingFlags.Instance
                        | BindingFlags.Static)
                    .Where(f => !f.FieldType.IsPointer)
                    .Select(Field.Get)
                    .ToArray());

            _properties = new Lazy<IProperty[]>(
                () =>
                _type.GetProperties(
                        BindingFlags.Public
                        | BindingFlags.NonPublic
                        | BindingFlags.Instance
                        | BindingFlags.Static)
                    .Where(p => !p.PropertyType.IsPointer)
                    .Select(Property.Get)
                    .ToArray());

            _fieldMap = new Lazy<IDictionary<string, IField>>(() => Fields.ToDictionary(f => f.Name));
            _propertyMap = new Lazy<IDictionary<string, IProperty>>(() => Properties.ToDictionary(p => p.Name));
        }

        public Type Type { get { return _type; } }
        public IConstructor[] Constructors { get { return _constructors.Value; } }
        public IField[] Fields { get { return _fields.Value; } }
        public IProperty[] Properties { get { return _properties.Value; } }

        internal IField GetField(string name)
        {
            return _fieldMap.Value[name];
        }

        internal IProperty GetProperty(string name)
        {
            return _propertyMap.Value[name];
        }
    }
}