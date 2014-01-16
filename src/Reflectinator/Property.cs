using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectinator
{
    public static class Property
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type, string>, IProperty> _propertiesMap = new ConcurrentDictionary<Tuple<Type, Type, Type, string>, IProperty>();

        public static IProperty Get(PropertyInfo propertyInfo)
        {
            return _propertiesMap.GetOrAdd(
                Tuple.Create(propertyInfo.DeclaringType, propertyInfo.PropertyType, propertyInfo.DeclaringType, propertyInfo.Name),
                t =>
                    (IProperty)(propertyInfo.IsStatic()
                        ? Activator.CreateInstance(typeof(StaticProperty<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { propertyInfo }, null)
                        : Activator.CreateInstance(typeof(Property<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { propertyInfo }, null)));
        }

        public static Property<TDeclaringType, TPropertyType> Get<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return (Property<TDeclaringType, TPropertyType>)_propertiesMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TPropertyType), propertyInfo.DeclaringType, propertyInfo.Name),
                t =>
                    propertyInfo.IsStatic()
                        ? new StaticProperty<TDeclaringType, TPropertyType>(propertyInfo)
                        : new Property<TDeclaringType, TPropertyType>(propertyInfo));
        }

        public static IStaticProperty GetStatic(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.IsStatic())
            {
                throw new ArgumentException("Cannot call GetStatic(PropertyInfo) on a non-static PropertyInfo.", "propertyInfo");
            }

            return (IStaticProperty)_propertiesMap.GetOrAdd(
                Tuple.Create(propertyInfo.DeclaringType, propertyInfo.PropertyType, propertyInfo.DeclaringType, propertyInfo.Name),
                t => (IProperty)Activator.CreateInstance(typeof(StaticProperty<,>).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { propertyInfo }, null));
        }

        public static StaticProperty<TDeclaringType, TPropertyType> GetStatic<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.IsStatic())
            {
                throw new ArgumentException("Cannot call GetStatic(PropertyInfo) on a non-static PropertyInfo.", "propertyInfo");
            }

            return (StaticProperty<TDeclaringType, TPropertyType>)_propertiesMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TPropertyType), propertyInfo.DeclaringType, propertyInfo.Name),
                t => new StaticProperty<TDeclaringType, TPropertyType>(propertyInfo));
        }
    }
}