using System;
using System.Reflection;

namespace Reflectinator
{
    public sealed class StaticField<TDeclaringType, TFieldType> : Field<TDeclaringType, TFieldType>, IStaticField
    {
        private readonly Lazy<Func<object>> _getValueAsStaticLooselyTyped;
        private readonly Lazy<Action<object>> _setValueAsStaticLooselyTyped;

        private readonly Lazy<Func<TFieldType>> _getValueAsStaticStronglyTyped;
        private readonly Lazy<Action<TFieldType>> _setValueAsStaticStronglyTyped;

        internal StaticField(FieldInfo fieldInfo) : base(fieldInfo)
        {
            _getValueAsStaticLooselyTyped = new Lazy<Func<object>>(() => () => ((IField)this).Get(null));
            _setValueAsStaticLooselyTyped = new Lazy<Action<object>>(() => value => ((IField)this).Set(null, value));
            _getValueAsStaticStronglyTyped = new Lazy<Func<TFieldType>>(() => () => Get(default(TDeclaringType)));
            _setValueAsStaticStronglyTyped = new Lazy<Action<TFieldType>>(() => value => Set(default(TDeclaringType), value));
        }

        Func<object> IStaticField.StaticGetFunc { get { return _getValueAsStaticLooselyTyped.Value; } }
        Action<object> IStaticField.StaticSetAction { get { return _setValueAsStaticLooselyTyped.Value; } }

        object IStaticField.Get() { return _getValueAsStaticLooselyTyped.Value(); }
        void IStaticField.Set(object value) { _setValueAsStaticLooselyTyped.Value(value); }

        public Func<TFieldType> GetStaticFunc { get { return _getValueAsStaticStronglyTyped.Value; } }
        public Action<TFieldType> SetStaticAction { get { return _setValueAsStaticStronglyTyped.Value; } }

        public TFieldType Get() { return _getValueAsStaticStronglyTyped.Value(); }
        public void Set(TFieldType value) { _setValueAsStaticStronglyTyped.Value(value); }
    }
}