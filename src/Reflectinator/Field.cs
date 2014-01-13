using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectinator
{
    public static class Field
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type, string>, IField> _fieldsMap = new ConcurrentDictionary<Tuple<Type, Type, Type, string>, IField>();

        // NOTE: See the 'NOTE:' above Property's Get factory method.
        public static IField Get(FieldInfo fieldInfo)
        {
            return _fieldsMap.GetOrAdd(
                Tuple.Create(fieldInfo.DeclaringType, fieldInfo.FieldType, fieldInfo.DeclaringType, fieldInfo.Name),
                t => (IField)Activator.CreateInstance(typeof(Field<,>).MakeGenericType(fieldInfo.DeclaringType, fieldInfo.FieldType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { fieldInfo }, null));
        }

        public static Field<TDeclaringType, TFieldType> Get<TDeclaringType, TFieldType>(FieldInfo fieldInfo)
        {
            return (Field<TDeclaringType, TFieldType>)_fieldsMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TFieldType), fieldInfo.DeclaringType, fieldInfo.Name),
                t => new Field<TDeclaringType, TFieldType>(fieldInfo));
        }
    }
}