using System.Reflection;

namespace Reflectinator
{
    public class CachedFieldInfo<TDeclaringType, TFieldType> : ICachedFieldInfo
    {
        private readonly FieldInfo _fieldInfo;
        private readonly ICachedType _fieldType;
        private readonly ICachedType _declaringType;

        public CachedFieldInfo(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
            _fieldType = CachedType.Create(fieldInfo.FieldType);
            _declaringType = CachedType.Create(fieldInfo.DeclaringType);
        }

        public FieldInfo FieldInfo { get { return _fieldInfo; } }
        public bool IsPublic { get { return _fieldInfo.IsPublic; } }
        public bool IsStatic { get { return _fieldInfo.IsStatic; } }
        public ICachedType FieldType { get { return _fieldType; } }
        public ICachedType DeclaringType { get { return _declaringType; } }

        object ICachedFieldInfo.GetValue(object obj)
        {
            // TODO: Implement for real
            return FieldInfo.GetValue(obj);
        }

        void ICachedFieldInfo.SetValue(object obj, object value)
        {
            // TODO: Implement for real
            FieldInfo.SetValue(obj, value);
        }

        public TFieldType GetValue(TDeclaringType obj)
        {
            return (TFieldType)((ICachedFieldInfo)this).GetValue(obj);
        }

        public void SetValue(TDeclaringType obj, TFieldType value)
        {
            ((ICachedFieldInfo)this).SetValue(obj, value);
        }
    }
}