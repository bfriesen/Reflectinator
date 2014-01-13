using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectinator
{
    public class Field : IField
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type, Type, string>, IField> _fieldsMap = new ConcurrentDictionary<Tuple<Type, Type, Type, string>, IField>();

        private readonly FieldInfo _fieldInfo;

        private readonly Lazy<ITypeCrawler> _fieldType;
        private readonly Lazy<ITypeCrawler> _declaringType;

        private readonly Lazy<Func<object, object>> _getValue;
        private readonly Lazy<Action<object, object>> _setValue;

        private readonly Lazy<Func<object>> _getValueAsStatic;
        private readonly Lazy<Action<object>> _setValueAsStatic;

        internal Field(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;

            _fieldType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(fieldInfo.FieldType));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(fieldInfo.DeclaringType));

            _getValue = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(fieldInfo));
            _setValue = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(fieldInfo));

            _getValueAsStatic = new Lazy<Func<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a field that is not static.");
                }

                var iThis = (IField)this;
                return () => iThis.Get(null);
            });
            _setValueAsStatic = new Lazy<Action<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(object value) on a field that is not static.");
                }

                var iThis = (IField)this;
                return value => iThis.Set(null, value);
            });
        }

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

        public string Name { get { return _fieldInfo.Name; } }
        public FieldInfo FieldInfo { get { return _fieldInfo; } }

        public bool IsPublic { get { return _fieldInfo.IsPublic; } }
        public bool IsStatic { get { return _fieldInfo.IsStatic; } }

        public bool IsReadOnly { get { return _fieldInfo.IsInitOnly || IsConstant; } }
        public bool IsConstant { get { return _fieldInfo.IsLiteral; } }

        public ITypeCrawler FieldType { get { return _fieldType.Value; } }
        public ITypeCrawler DeclaringType { get { return _declaringType.Value; } }

        Func<object, object> IField.GetFunc { get { return _getValue.Value; } }
        Action<object, object> IField.SetAction { get { return _setValue.Value; } }

        Func<object> IField.GetStaticFunc { get { return _getValueAsStatic.Value; } }
        Action<object> IField.SetStaticAction { get { return _setValueAsStatic.Value; } }

        object IField.Get(object instance) { return _getValue.Value(instance); }
        void IField.Set(object instance, object value) { _setValue.Value(instance, value); }

        object IField.Get() { return _getValueAsStatic.Value(); }
        void IField.Set(object value) { _setValueAsStatic.Value(value); }
    }
}