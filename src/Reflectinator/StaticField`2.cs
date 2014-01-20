using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public sealed class StaticField<TDeclaringType, TFieldType> : Field<TDeclaringType, TFieldType>, IStaticField
    {
        private readonly Lazy<Expression<Func<object>>> _getValueAsStaticLooselyTypedExpression;
        private readonly Lazy<Expression<Action<object>>> _setValueAsStaticLooselyTypedExpression;

        private readonly Lazy<Func<object>> _getValueAsStaticLooselyTyped;
        private readonly Lazy<Action<object>> _setValueAsStaticLooselyTyped;

        private readonly Lazy<Expression<Func<TFieldType>>> _getValueAsStaticStronglyTypedExpression;
        private readonly Lazy<Expression<Action<TFieldType>>> _setValueAsStaticStronglyTypedExpression;

        private readonly Lazy<Func<TFieldType>> _getValueAsStaticStronglyTyped;
        private readonly Lazy<Action<TFieldType>> _setValueAsStaticStronglyTyped;

        internal StaticField(FieldInfo fieldInfo) : base(fieldInfo)
        {
            _getValueAsStaticLooselyTypedExpression = new Lazy<Expression<Func<object>>>(() => ExpressionFactory.CreateStaticGetValueFuncExpression(fieldInfo));
            _setValueAsStaticLooselyTypedExpression = new Lazy<Expression<Action<object>>>(() => ExpressionFactory.CreateStaticSetValueActionExpression(fieldInfo));
            _getValueAsStaticStronglyTypedExpression = new Lazy<Expression<Func<TFieldType>>>(() => ExpressionFactory.CreateStaticGetValueFuncExpression<TFieldType>(fieldInfo));
            _setValueAsStaticStronglyTypedExpression = new Lazy<Expression<Action<TFieldType>>>(() => ExpressionFactory.CreateStaticSetValueActionExpression<TFieldType>(fieldInfo));

            _getValueAsStaticLooselyTyped = new Lazy<Func<object>>(() => _getValueAsStaticLooselyTypedExpression.Value.Compile());
            _setValueAsStaticLooselyTyped = new Lazy<Action<object>>(() => _setValueAsStaticLooselyTypedExpression.Value.Compile());
            _getValueAsStaticStronglyTyped = new Lazy<Func<TFieldType>>(() => _getValueAsStaticStronglyTypedExpression.Value.Compile());
            _setValueAsStaticStronglyTyped = new Lazy<Action<TFieldType>>(() => _setValueAsStaticStronglyTypedExpression.Value.Compile());
        }

        Expression<Func<object>> IStaticField.StaticGetFuncExpression { get { return _getValueAsStaticLooselyTypedExpression.Value; } }
        Expression<Action<object>> IStaticField.StaticSetActionExpression { get { return _setValueAsStaticLooselyTypedExpression.Value; } }

        Func<object> IStaticField.StaticGetFunc { get { return _getValueAsStaticLooselyTyped.Value; } }
        Action<object> IStaticField.StaticSetAction { get { return _setValueAsStaticLooselyTyped.Value; } }

        object IStaticField.Get() { return _getValueAsStaticLooselyTyped.Value(); }
        void IStaticField.Set(object value) { _setValueAsStaticLooselyTyped.Value(value); }

        public Expression<Func<TFieldType>> GetStaticFuncExpression { get { return _getValueAsStaticStronglyTypedExpression.Value; } }
        public Expression<Action<TFieldType>> SetStaticActionExpression { get { return _setValueAsStaticStronglyTypedExpression.Value; } }

        public Func<TFieldType> GetStaticFunc { get { return _getValueAsStaticStronglyTyped.Value; } }
        public Action<TFieldType> SetStaticAction { get { return _setValueAsStaticStronglyTyped.Value; } }

        public TFieldType Get() { return _getValueAsStaticStronglyTyped.Value(); }
        public void Set(TFieldType value) { _setValueAsStaticStronglyTyped.Value(value); }
    }
}