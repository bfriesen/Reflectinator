using System;
using System.Reflection;

namespace Reflectinator
{
    public sealed class Field<TDeclaringType, TFieldType> : IField
    {
        private readonly FieldInfo _fieldInfo;

        private readonly Lazy<ITypeCrawler> _fieldType;
        private readonly Lazy<ITypeCrawler> _declaringType;

        private readonly Lazy<Func<object, object>> _getValueLooselyTyped;
        private readonly Lazy<Action<object, object>> _setValueLooselyTyped;

        private readonly Lazy<Func<object>> _getValueAsStaticLooselyTyped;
        private readonly Lazy<Action<object>> _setValueAsStaticLooselyTyped;

        private readonly Lazy<Func<TDeclaringType, TFieldType>> _getValueStronglyTyped;
        private readonly Lazy<Action<TDeclaringType, TFieldType>> _setValueStronglyTyped;

        private readonly Lazy<Func<TFieldType>> _getValueAsStaticStronglyTyped;
        private readonly Lazy<Action<TFieldType>> _setValueAsStaticStronglyTyped;

        internal Field(FieldInfo fieldInfo)
        {
            if (!typeof(TDeclaringType).IsAssignableFrom(fieldInfo.DeclaringType))
            {
                throw new ArgumentException("TDeclaringType is not assignable from the DeclaringType of the fieldInfo", "fieldInfo");
            }

            if (!typeof(TFieldType).IsAssignableFrom(fieldInfo.FieldType))
            {
                throw new ArgumentException("TFieldType is not assignable from the FieldType of the fieldInfo", "fieldInfo");
            }

            _fieldInfo = fieldInfo;

            _fieldType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(fieldInfo.FieldType));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(fieldInfo.DeclaringType));

            _getValueLooselyTyped = new Lazy<Func<object, object>>(() => FuncFactory.CreateGetValueFunc(fieldInfo));
            _setValueLooselyTyped = new Lazy<Action<object, object>>(() => FuncFactory.CreateSetValueFunc(fieldInfo));

            _getValueAsStaticLooselyTyped = new Lazy<Func<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a field that is not static.");
                }

                var iThis = (IField)this;
                return () => iThis.Get(null);
            });

            _setValueAsStaticLooselyTyped = new Lazy<Action<object>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(object value) on a field that is not static.");
                }

                var iThis = (IField)this;
                return value => iThis.Set(null, value);
            });

            _getValueStronglyTyped = new Lazy<Func<TDeclaringType, TFieldType>>(() => FuncFactory.CreateGetValueFunc<TDeclaringType, TFieldType>(fieldInfo));
            _setValueStronglyTyped = new Lazy<Action<TDeclaringType, TFieldType>>(() => FuncFactory.CreateSetValueFunc<TDeclaringType, TFieldType>(fieldInfo));

            _getValueAsStaticStronglyTyped = new Lazy<Func<TFieldType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a field that is not static.");
                }

                return () => Get(default(TDeclaringType));
            });
            _setValueAsStaticStronglyTyped = new Lazy<Action<TFieldType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(TFieldValue value) on a field that is not static.");
                }

                return value => Set(default(TDeclaringType), value);
            });
        }

        public string Name { get { return _fieldInfo.Name; } }
        public FieldInfo FieldInfo { get { return _fieldInfo; } }

        public bool IsPublic { get { return _fieldInfo.IsPublic; } }
        public bool IsStatic { get { return _fieldInfo.IsStatic; } }

        public bool IsReadOnly { get { return _fieldInfo.IsInitOnly || IsConstant; } }
        public bool IsConstant { get { return _fieldInfo.IsLiteral; } }

        public ITypeCrawler FieldType { get { return _fieldType.Value; } }
        public ITypeCrawler DeclaringType { get { return _declaringType.Value; } }

        Func<object, object> IField.GetFunc { get { return _getValueLooselyTyped.Value; } }
        Action<object, object> IField.SetAction { get { return _setValueLooselyTyped.Value; } }

        Func<object> IField.GetStaticFunc { get { return _getValueAsStaticLooselyTyped.Value; } }
        Action<object> IField.SetStaticAction { get { return _setValueAsStaticLooselyTyped.Value; } }

        object IField.Get(object instance) { return _getValueLooselyTyped.Value(instance); }
        void IField.Set(object instance, object value) { _setValueLooselyTyped.Value(instance, value); }

        object IField.Get() { return _getValueAsStaticLooselyTyped.Value(); }
        void IField.Set(object value) { _setValueAsStaticLooselyTyped.Value(value); }

        public Func<TDeclaringType, TFieldType> GetFunc { get { return _getValueStronglyTyped.Value; } }
        public Action<TDeclaringType, TFieldType> SetAction { get { return _setValueStronglyTyped.Value; } }

        public Func<TFieldType> GetStaticFunc { get { return _getValueAsStaticStronglyTyped.Value; } }
        public Action<TFieldType> SetStaticAction { get { return _setValueAsStaticStronglyTyped.Value; } }

        public TFieldType Get(TDeclaringType instance) { return _getValueStronglyTyped.Value(instance); }
        public void Set(TDeclaringType instance, TFieldType value) { _setValueStronglyTyped.Value(instance, value); }

        public TFieldType Get() { return _getValueAsStaticStronglyTyped.Value(); }
        public void Set(TFieldType value) { _setValueAsStaticStronglyTyped.Value(value); }
    }
}