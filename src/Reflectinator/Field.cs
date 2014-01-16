using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectinator
{
    public static class Field
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type, string>, IField> _fieldsMap = new ConcurrentDictionary<Tuple<Type, Type, Type, string>, IField>();

        public static IField Get(FieldInfo fieldInfo)
        {
            return _fieldsMap.GetOrAdd(
                Tuple.Create(fieldInfo.DeclaringType, fieldInfo.FieldType, fieldInfo.DeclaringType, fieldInfo.Name),
                t =>
                    (IField)(fieldInfo.IsStatic
                        ? Activator.CreateInstance(typeof(StaticField<,>).MakeGenericType(fieldInfo.DeclaringType, fieldInfo.FieldType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { fieldInfo }, null)
                        : Activator.CreateInstance(typeof(Field<,>).MakeGenericType(fieldInfo.DeclaringType, fieldInfo.FieldType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { fieldInfo }, null)));
        }

        public static Field<TDeclaringType, TFieldType> Get<TDeclaringType, TFieldType>(FieldInfo fieldInfo)
        {
            return (Field<TDeclaringType, TFieldType>)_fieldsMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TFieldType), fieldInfo.DeclaringType, fieldInfo.Name),
                t =>
                    fieldInfo.IsStatic
                        ? new StaticField<TDeclaringType, TFieldType>(fieldInfo)
                        : new Field<TDeclaringType, TFieldType>(fieldInfo));
        }

        public static IStaticField GetStatic(FieldInfo fieldInfo)
        {
            if (!fieldInfo.IsStatic)
            {
                throw new ArgumentException("Cannot call GetStatic(FieldInfo) on a non-static FieldInfo.", "fieldInfo");
            }

            return (IStaticField)_fieldsMap.GetOrAdd(
                Tuple.Create(fieldInfo.DeclaringType, fieldInfo.FieldType, fieldInfo.DeclaringType, fieldInfo.Name),
                t => (IField)Activator.CreateInstance(typeof(StaticField<,>).MakeGenericType(fieldInfo.DeclaringType, fieldInfo.FieldType), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { fieldInfo }, null));
        }

        public static StaticField<TDeclaringType, TFieldType> GetStatic<TDeclaringType, TFieldType>(FieldInfo fieldInfo)
        {
            return (StaticField<TDeclaringType, TFieldType>)_fieldsMap.GetOrAdd(
                Tuple.Create(typeof(TDeclaringType), typeof(TFieldType), fieldInfo.DeclaringType, fieldInfo.Name),
                t => new StaticField<TDeclaringType, TFieldType>(fieldInfo));
        }
    }
}