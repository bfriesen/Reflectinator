using System;
using System.Reflection;

namespace Reflectinator
{
    public class Field : IField
    {
        private readonly FieldInfo _fieldInfo;

        private readonly Lazy<ITypeCrawler> _fieldType;
        private readonly Lazy<ITypeCrawler> _declaringType;

        private readonly Lazy<Func<object, object>> _getValue;
        private readonly Lazy<Action<object, object>> _setValue;

        private readonly Lazy<Func<object>> _getValueAsStatic;
        private readonly Lazy<Action<object>> _setValueAsStatic;

        protected Field(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;

            _fieldType = new Lazy<ITypeCrawler>(() => TypeCrawler.Create(fieldInfo.FieldType));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Create(fieldInfo.DeclaringType));

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

        // NOTE: See the 'NOTE:' above Property's Create factory method.
        public static IField Create(FieldInfo fieldInfo)
        {
            return new Field(fieldInfo);
        }

        public static Field<TDeclaringType, TFieldType> Create<TDeclaringType, TFieldType>(FieldInfo fieldInfo)
        {
            return new Field<TDeclaringType, TFieldType>(fieldInfo);
        }

        public string Name { get { return _fieldInfo.Name; } }
        public FieldInfo FieldInfo { get { return _fieldInfo; } }

        public bool IsPublic { get { return _fieldInfo.IsPublic; } }
        public bool IsStatic { get { return _fieldInfo.IsStatic; } }

        public bool IsReadOnly { get { return _fieldInfo.IsInitOnly || IsConstant; } }
        public bool IsConstant { get { return _fieldInfo.IsLiteral; } }

        public ITypeCrawler FieldType { get { return _fieldType.Value; } }
        public ITypeCrawler DeclaringType { get { return _declaringType.Value; } }

        object IField.Get(object instance) { return _getValue.Value(instance); }
        void IField.Set(object instance, object value) { _setValue.Value(instance, value); }

        object IField.Get() { return _getValueAsStatic.Value(); }
        void IField.Set(object value) { _setValueAsStatic.Value(value); }
    }
}