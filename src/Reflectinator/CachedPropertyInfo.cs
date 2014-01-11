using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public abstract class CachedPropertyInfo : ICachedPropertyInfo
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly bool _isPublic;
        private readonly bool _isStatic;
        private readonly ICachedType _propertyType;
        private readonly ICachedType _declaringType;

        private readonly Lazy<Func<object, object>> _getValueFunc;
        private readonly Lazy<Action<object, object>> _setValueFunc;

        protected CachedPropertyInfo(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            _isPublic = (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsPublic)
                || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsPublic);
            _isStatic = (propertyInfo.CanRead && propertyInfo.GetGetMethod(true).IsStatic)
                || (propertyInfo.CanWrite && propertyInfo.GetSetMethod(true).IsStatic);
            _propertyType = CachedType.Create(propertyInfo.PropertyType);
            _declaringType = CachedType.Create(propertyInfo.DeclaringType);

            _getValueFunc = new Lazy<Func<object, object>>(CreateGetValueFunc);
            _setValueFunc = new Lazy<Action<object, object>>(CreateSetValueFunc);
        }

        public PropertyInfo PropertyInfo { get { return _propertyInfo; } }
        public string Name { get { return _propertyInfo.Name; } }
        public bool IsPublic { get { return _isPublic; } }
        public bool IsStatic { get { return _isStatic; } }
        public bool CanRead { get { return _propertyInfo.CanRead; } }
        public bool CanWrite { get { return _propertyInfo.CanWrite; } }
        public ICachedType PropertyType { get { return _propertyType; } }
        public ICachedType DeclaringType { get { return _declaringType; } }

        Func<object, object> ICachedPropertyInfo.Get { get { return _getValueFunc.Value; } }
        Action<object, object> ICachedPropertyInfo.Set { get { return _setValueFunc.Value; } }

        private Func<object, object> CreateGetValueFunc()
        {
            if (CanRead)
            {
                var method = PropertyInfo.GetGetMethod();

                var insanceParameter = Expression.Parameter(typeof(object), "instance");

                var instanceCast =
                    method.DeclaringType.IsValueType
                        ? Expression.Convert(insanceParameter, method.DeclaringType)
                        : Expression.TypeAs(insanceParameter, method.DeclaringType);

                var call =
                    Expression.Call(
                        instanceCast,
                        method);

                var expression = Expression.Lambda<Func<object, object>>(
                    call,
                    insanceParameter);

                return expression.Compile();
            }

            throw new NotImplementedException("Cannot read from property.");
        }

        private Action<object, object> CreateSetValueFunc()
        {
            if (CanWrite)
            {
                var method = PropertyInfo.GetSetMethod();

                var instanceParameter = Expression.Parameter(typeof(object), "instance");
                var valueParameter = Expression.Parameter(typeof(object), "value");

                var instanceCast =
                    method.DeclaringType.IsValueType
                        ? Expression.Convert(instanceParameter, method.DeclaringType)
                        : Expression.TypeAs(instanceParameter, method.DeclaringType);

                var valueParameterType = method.GetParameters().Single().ParameterType;
                var valueCast = valueParameterType.IsValueType
                    ? Expression.Convert(valueParameter, valueParameterType)
                    : Expression.TypeAs(valueParameter, valueParameterType);

                var call =
                    Expression.Call(
                        instanceCast,
                        method,
                        valueCast);

                var expression = Expression.Lambda<Action<object, object>>(
                    call,
                    instanceParameter,
                    valueParameter);

                return expression.Compile();
            }

            throw new NotImplementedException("Cannot write to property.");
        }
    }
}