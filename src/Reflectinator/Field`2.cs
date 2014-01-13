using System;
using System.Reflection;

namespace Reflectinator
{
    public class Field<TDeclaringType, TFieldType> : Field
    {
        private readonly Lazy<Func<TDeclaringType, TFieldType>> _getValue;
        private readonly Lazy<Action<TDeclaringType, TFieldType>> _setValue;

        private readonly Lazy<Func<TFieldType>> _getValueAsStatic;
        private readonly Lazy<Action<TFieldType>> _setValueAsStatic;

        public Field(FieldInfo fieldInfo)
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

            _getValue = new Lazy<Func<TDeclaringType, TFieldType>>(() => FuncFactory.CreateGetValueFunc<TDeclaringType, TFieldType>(fieldInfo));
            _setValue = new Lazy<Action<TDeclaringType, TFieldType>>(() => FuncFactory.CreateSetValueFunc<TDeclaringType, TFieldType>(fieldInfo));

            _getValueAsStatic = new Lazy<Func<TFieldType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Get() on a field that is not static.");
                }

                return () => Get(default(TDeclaringType));
            });
            _setValueAsStatic = new Lazy<Action<TFieldType>>(() =>
            {
                if (!IsStatic)
                {
                    throw new InvalidOperationException("Cannot call Set(TFieldValue value) on a field that is not static.");
                }

                return value => Set(default(TDeclaringType), value);
            });
        }

        public Func<TDeclaringType, TFieldType> GetFunc { get { return _getValue.Value; } }
        public Action<TDeclaringType, TFieldType> SetAction { get { return _setValue.Value; } }

        public Func<TFieldType> GetStaticFunc { get { return _getValueAsStatic.Value; } }
        public Action<TFieldType> SetStaticAction { get { return _setValueAsStatic.Value; } }

        public TFieldType Get(TDeclaringType instance) { return _getValue.Value(instance); }
        public void Set(TDeclaringType instance, TFieldType value) { _setValue.Value(instance, value); }

        public TFieldType Get() { return _getValueAsStatic.Value(); }
        public void Set(TFieldType value) { _setValueAsStatic.Value(value); }
    }
}