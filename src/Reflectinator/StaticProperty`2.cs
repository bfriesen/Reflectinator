using System;
using System.Reflection;

namespace Reflectinator
{
    public sealed class StaticProperty<TDeclaringType, TPropertyType> : Property<TDeclaringType, TPropertyType>, IStaticProperty
    {
        private readonly Lazy<Func<object>> _getValueAsStaticLooselyTyped;
        private readonly Lazy<Action<object>> _setValueAsStaticLooselyTyped;

        private readonly Lazy<Func<TPropertyType>> _getValueAsStaticStronglyTyped;
        private readonly Lazy<Action<TPropertyType>> _setValueAsStaticStronglyTyped;

        internal StaticProperty(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            _getValueAsStaticLooselyTyped = new Lazy<Func<object>>(() => FuncFactory.CreateStaticGetValueFunc(propertyInfo));
            _setValueAsStaticLooselyTyped = new Lazy<Action<object>>(() => FuncFactory.CreateStaticSetValueAction(propertyInfo));
            _getValueAsStaticStronglyTyped = new Lazy<Func<TPropertyType>>(() => FuncFactory.CreateStaticGetValueFunc<TPropertyType>(propertyInfo));
            _setValueAsStaticStronglyTyped = new Lazy<Action<TPropertyType>>(() => FuncFactory.CreateStaticSetValueAction<TPropertyType>(propertyInfo));
        }

        public override bool IsStatic { get { return true; } }

        Func<object> IStaticProperty.StaticGetFunc { get { return _getValueAsStaticLooselyTyped.Value; } }
        Action<object> IStaticProperty.StaticSetAction { get { return _setValueAsStaticLooselyTyped.Value; } }

        object IStaticProperty.Get() { return _getValueAsStaticLooselyTyped.Value(); }
        void IStaticProperty.Set(object value) { _setValueAsStaticLooselyTyped.Value(value); }

        public Func<TPropertyType> StaticGetFunc { get { return _getValueAsStaticStronglyTyped.Value; } }
        public Action<TPropertyType> StaticSetAction { get { return _setValueAsStaticStronglyTyped.Value; } }

        public TPropertyType Get() { return _getValueAsStaticStronglyTyped.Value(); }
        public void Set(TPropertyType value) { _setValueAsStaticStronglyTyped.Value(value); }
    }
}