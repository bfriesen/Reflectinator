using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectinator
{
    public static class Property
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type, string>, IProperty> _propertiesMap = new ConcurrentDictionary<Tuple<Type, Type, Type, string>, IProperty>();

        // NOTE: We're returning the interface because the Get and Set properties are implemented explicitly.
        //       If we didn't do this, then the object returned wouldn't have a visible Get or Set method. And
        //       that wouldn't be a very nice API, now would it?
        public static IProperty Get(PropertyInfo propertyInfo)
        {
            return _propertiesMap.GetOrAdd(
                Tuple.Create(propertyInfo.DeclaringType, propertyInfo.PropertyType, propertyInfo.DeclaringType, propertyInfo.Name),
                t => (IProperty)Activator.CreateInstance(typeof(Property<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { propertyInfo }, null));
        }

        public static Property<TDeclaringType, TPropertyType> Get<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return (Property<TDeclaringType, TPropertyType>)_propertiesMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TPropertyType), propertyInfo.DeclaringType, propertyInfo.Name),
                t => new Property<TDeclaringType, TPropertyType>(propertyInfo));
        }
    }
}