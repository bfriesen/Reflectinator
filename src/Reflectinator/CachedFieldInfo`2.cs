﻿using System;
using System.Reflection;

namespace Reflectinator
{
    public class CachedFieldInfo<TDeclaringType, TFieldType> : ICachedFieldInfo
    {
        private readonly FieldInfo _fieldInfo;
        private readonly Lazy<ICachedType> _fieldType;
        private readonly Lazy<ICachedType> _declaringType;

        public CachedFieldInfo(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
            _fieldType = new Lazy<ICachedType>(() => CachedType.Create(fieldInfo.FieldType));
            _declaringType = new Lazy<ICachedType>(() => CachedType.Create(fieldInfo.DeclaringType));
        }

        public FieldInfo FieldInfo { get { return _fieldInfo; } }
        public string Name { get { return _fieldInfo.Name; } }
        public bool IsPublic { get { return _fieldInfo.IsPublic; } }
        public bool IsStatic { get { return _fieldInfo.IsStatic; } }
        public bool IsReadOnly { get { return _fieldInfo.IsInitOnly || IsConstant; } }
        public bool IsConstant { get { return _fieldInfo.IsLiteral; } }
        public ICachedType FieldType { get { return _fieldType.Value; } }
        public ICachedType DeclaringType { get { return _declaringType.Value; } }

        object ICachedFieldInfo.Get(object obj)
        {
            // TODO: Implement for real
            return FieldInfo.GetValue(obj);
        }

        void ICachedFieldInfo.Set(object obj, object value)
        {
            // TODO: Implement for real
            FieldInfo.SetValue(obj, value);
        }

        public TFieldType Get(TDeclaringType obj)
        {
            return (TFieldType)((ICachedFieldInfo)this).Get(obj);
        }

        public void Set(TDeclaringType obj, TFieldType value)
        {
            ((ICachedFieldInfo)this).Set(obj, value);
        }
    }
}