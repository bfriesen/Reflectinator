using System;
using System.Reflection;

namespace Reflectinator
{
    public class CachedPropertyInfo<TDeclaringType, TPropertyType> : CachedPropertyInfo
    {
        private readonly Lazy<Func<TDeclaringType, TPropertyType>> _getValueFunc;
        private readonly Lazy<Action<TDeclaringType, TPropertyType>> _setValueFunc;

        public CachedPropertyInfo(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            _getValueFunc = new Lazy<Func<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateGetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));
            _setValueFunc = new Lazy<Action<TDeclaringType, TPropertyType>>(() => FuncFactory.CreateSetValueFunc<TDeclaringType, TPropertyType>(propertyInfo));
        }

        public Func<TDeclaringType, TPropertyType> Get { get { return _getValueFunc.Value; } }
        public Action<TDeclaringType, TPropertyType> Set { get { return _setValueFunc.Value; } }
    }
}