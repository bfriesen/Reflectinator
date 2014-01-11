using System;
using System.Linq.Expressions;
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
            _getValueFunc = new Lazy<Func<TDeclaringType, TPropertyType>>(CreateGetValueFunc);
            _setValueFunc = new Lazy<Action<TDeclaringType, TPropertyType>>(CreateSetValueFunc);
        }

        public Func<TDeclaringType, TPropertyType> Get { get { return _getValueFunc.Value; } }
        public Action<TDeclaringType, TPropertyType> Set { get { return _setValueFunc.Value; } }

        private Func<TDeclaringType, TPropertyType> CreateGetValueFunc()
        {
            if (CanRead)
            {
                var method = PropertyInfo.GetGetMethod();

                var insanceParameter = Expression.Parameter(typeof(TDeclaringType), "instance");

                var call =
                    Expression.Call(
                        insanceParameter,
                        method);

                var expression = Expression.Lambda<Func<TDeclaringType, TPropertyType>>(
                    call,
                    insanceParameter);

                return expression.Compile();
            }

            throw new MemberAccessException(string.Format("Cannot read from property: {0}.{1}",
                PropertyInfo.DeclaringType.FullName, PropertyInfo.Name));
        }

        private Action<TDeclaringType, TPropertyType> CreateSetValueFunc()
        {
            if (CanWrite)
            {
                var method = PropertyInfo.GetSetMethod();

                var instanceParameter = Expression.Parameter(typeof(TDeclaringType), "instance");
                var valueParameter = Expression.Parameter(typeof(TPropertyType), "value");

                var call =
                    Expression.Call(
                        instanceParameter,
                        method,
                        valueParameter);

                var expression = Expression.Lambda<Action<TDeclaringType, TPropertyType>>(
                    call,
                    instanceParameter,
                    valueParameter);

                return expression.Compile();
            }

            throw new MemberAccessException(string.Format("Cannot write to property: {0}.{1}",
                PropertyInfo.DeclaringType.FullName, PropertyInfo.Name));
        }
    }
}