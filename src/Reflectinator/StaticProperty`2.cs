using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public sealed class StaticProperty<TDeclaringType, TPropertyType> : Property<TDeclaringType, TPropertyType>, IStaticProperty
    {
        private readonly Lazy<Expression<Func<object>>> _getValueAsStaticLooselyTypedExpression;
        private readonly Lazy<Expression<Action<object>>> _setValueAsStaticLooselyTypedExpression;

        private readonly Lazy<Func<object>> _getValueAsStaticLooselyTyped;
        private readonly Lazy<Action<object>> _setValueAsStaticLooselyTyped;

        private readonly Lazy<Expression<Func<TPropertyType>>> _getValueAsStaticStronglyTypedExpression;
        private readonly Lazy<Expression<Action<TPropertyType>>> _setValueAsStaticStronglyTypedExpression;

        private readonly Lazy<Func<TPropertyType>> _getValueAsStaticStronglyTyped;
        private readonly Lazy<Action<TPropertyType>> _setValueAsStaticStronglyTyped;

        internal StaticProperty(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            _getValueAsStaticLooselyTypedExpression = new Lazy<Expression<Func<object>>>(() => ExpressionFactory.CreateStaticGetValueFuncExpression(propertyInfo));
            _setValueAsStaticLooselyTypedExpression = new Lazy<Expression<Action<object>>>(() => ExpressionFactory.CreateStaticSetValueActionExpression(propertyInfo));
            _getValueAsStaticStronglyTypedExpression = new Lazy<Expression<Func<TPropertyType>>>(() => ExpressionFactory.CreateStaticGetValueFuncExpression<TPropertyType>(propertyInfo));
            _setValueAsStaticStronglyTypedExpression = new Lazy<Expression<Action<TPropertyType>>>(() => ExpressionFactory.CreateStaticSetValueActionExpression<TPropertyType>(propertyInfo));

            _getValueAsStaticLooselyTyped = new Lazy<Func<object>>(() => _getValueAsStaticLooselyTypedExpression.Value.Compile());
            _setValueAsStaticLooselyTyped = new Lazy<Action<object>>(() => _setValueAsStaticLooselyTypedExpression.Value.Compile());
            _getValueAsStaticStronglyTyped = new Lazy<Func<TPropertyType>>(() => _getValueAsStaticStronglyTypedExpression.Value.Compile());
            _setValueAsStaticStronglyTyped = new Lazy<Action<TPropertyType>>(() => _setValueAsStaticStronglyTypedExpression.Value.Compile());
        }

        public override bool IsStatic { get { return true; } }

        Expression<Func<object>> IStaticProperty.StaticGetFuncExpression { get { return _getValueAsStaticLooselyTypedExpression.Value; } }
        Expression<Action<object>> IStaticProperty.StaticSetActionExpression { get { return _setValueAsStaticLooselyTypedExpression.Value; } }

        Func<object> IStaticProperty.StaticGetFunc { get { return _getValueAsStaticLooselyTyped.Value; } }
        Action<object> IStaticProperty.StaticSetAction { get { return _setValueAsStaticLooselyTyped.Value; } }

        object IStaticProperty.Get() { return _getValueAsStaticLooselyTyped.Value(); }
        void IStaticProperty.Set(object value) { _setValueAsStaticLooselyTyped.Value(value); }

        public Expression<Func<TPropertyType>> StaticGetFuncExpression { get { return _getValueAsStaticStronglyTypedExpression.Value; } }
        public Expression<Action<TPropertyType>> StaticSetActionExpression { get { return _setValueAsStaticStronglyTypedExpression.Value; } }

        public Func<TPropertyType> StaticGetFunc { get { return _getValueAsStaticStronglyTyped.Value; } }
        public Action<TPropertyType> StaticSetAction { get { return _setValueAsStaticStronglyTyped.Value; } }

        public TPropertyType Get() { return _getValueAsStaticStronglyTyped.Value(); }
        public void Set(TPropertyType value) { _setValueAsStaticStronglyTyped.Value(value); }
    }
}