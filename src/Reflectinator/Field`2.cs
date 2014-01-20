using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public class Field<TDeclaringType, TFieldType> : Member, IField
    {
        private readonly FieldInfo _fieldInfo;

        private readonly Lazy<ITypeCrawler> _fieldType;

        private readonly Lazy<Expression<Func<object, object>>> _getValueLooselyTypedExpression;
        private readonly Lazy<Expression<Action<object, object>>> _setValueLooselyTypedExpression;

        private readonly Lazy<Func<object, object>> _getValueLooselyTyped;
        private readonly Lazy<Action<object, object>> _setValueLooselyTyped;

        private readonly Lazy<Expression<Func<TDeclaringType, TFieldType>>> _getValueStronglyTypedExpression;
        private readonly Lazy<Expression<Action<TDeclaringType, TFieldType>>> _setValueStronglyTypedExpression;

        private readonly Lazy<Func<TDeclaringType, TFieldType>> _getValueStronglyTyped;
        private readonly Lazy<Action<TDeclaringType, TFieldType>> _setValueStronglyTyped;

        internal Field(FieldInfo fieldInfo)
            : base(fieldInfo)
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

            _getValueLooselyTypedExpression = new Lazy<Expression<Func<object, object>>>(() => ExpressionFactory.CreateGetValueFuncExpression(fieldInfo));
            _setValueLooselyTypedExpression = new Lazy<Expression<Action<object, object>>>(() => ExpressionFactory.CreateSetValueFuncExpression(fieldInfo));
            _getValueStronglyTypedExpression = new Lazy<Expression<Func<TDeclaringType, TFieldType>>>(() => ExpressionFactory.CreateGetValueFuncExpression<TDeclaringType, TFieldType>(fieldInfo));
            _setValueStronglyTypedExpression = new Lazy<Expression<Action<TDeclaringType, TFieldType>>>(() => ExpressionFactory.CreateSetValueFuncExpression<TDeclaringType, TFieldType>(fieldInfo));

            _getValueLooselyTyped = new Lazy<Func<object, object>>(() => _getValueLooselyTypedExpression.Value.Compile());
            _setValueLooselyTyped = new Lazy<Action<object, object>>(() => _setValueLooselyTypedExpression.Value.Compile());
            _getValueStronglyTyped = new Lazy<Func<TDeclaringType, TFieldType>>(() => _getValueStronglyTypedExpression.Value.Compile());
            _setValueStronglyTyped = new Lazy<Action<TDeclaringType, TFieldType>>(() => _setValueStronglyTypedExpression.Value.Compile());
        }

        public string Name { get { return _fieldInfo.Name; } }
        public FieldInfo FieldInfo { get { return _fieldInfo; } }

        public override bool IsPublic { get { return _fieldInfo.IsPublic; } }
        public override bool IsStatic { get { return _fieldInfo.IsStatic; } }

        public bool IsReadOnly { get { return _fieldInfo.IsInitOnly || IsConstant; } }
        public bool IsConstant { get { return _fieldInfo.IsLiteral; } }

        public ITypeCrawler FieldType { get { return _fieldType.Value; } }

        Expression<Func<object, object>> IField.GetFuncExpression { get { return _getValueLooselyTypedExpression.Value; } }
        Expression<Action<object, object>> IField.SetActionExpression { get { return _setValueLooselyTypedExpression.Value; } }

        Func<object, object> IField.GetFunc { get { return _getValueLooselyTyped.Value; } }
        Action<object, object> IField.SetAction { get { return _setValueLooselyTyped.Value; } }

        object IField.Get(object instance) { return _getValueLooselyTyped.Value(instance); }
        void IField.Set(object instance, object value) { _setValueLooselyTyped.Value(instance, value); }

        public Expression<Func<TDeclaringType, TFieldType>> GetFuncExpression { get { return _getValueStronglyTypedExpression.Value; } }
        public Expression<Action<TDeclaringType, TFieldType>> SetActionExpression { get { return _setValueStronglyTypedExpression.Value; } }

        public Func<TDeclaringType, TFieldType> GetFunc { get { return _getValueStronglyTyped.Value; } }
        public Action<TDeclaringType, TFieldType> SetAction { get { return _setValueStronglyTyped.Value; } }

        public TFieldType Get(TDeclaringType instance) { return _getValueStronglyTyped.Value(instance); }
        public void Set(TDeclaringType instance, TFieldType value) { _setValueStronglyTyped.Value(instance, value); }
    }
}