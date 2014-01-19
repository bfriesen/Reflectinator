using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public static partial class FuncFactory
    {
        #region Field

        public static Func<object, object> CreateGetValueFunc(FieldInfo fieldInfo)
        {
            return CreateGetValueFunc<object, object>(fieldInfo);
        }

        public static Func<TInstanceType, TReturnType> CreateGetValueFunc<TInstanceType, TReturnType>(FieldInfo fieldInfo)
        {
            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var field = GetField(fieldInfo, typeof(TInstanceType), instanceParameter);

            var body = field.Coerce(fieldInfo.FieldType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TInstanceType, TReturnType>>(
                body,
                instanceParameter);
            return expression.Compile();
        }

        public static Func<object> CreateStaticGetValueFunc(FieldInfo fieldInfo)
        {
            return CreateStaticGetValueFunc<object>(fieldInfo);
        }

        public static Func<TReturnType> CreateStaticGetValueFunc<TReturnType>(FieldInfo fieldInfo)
        {
            var field = Expression.Field(null, fieldInfo);
            var body = field.Coerce(fieldInfo.FieldType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TReturnType>>(body);
            return expression.Compile();
        }

        public static Action<object, object> CreateSetValueFunc(FieldInfo fieldInfo)
        {
            return CreateSetValueFunc<object, object>(fieldInfo);
        }

        public static Action<TInstanceType, TValueType> CreateSetValueFunc<TInstanceType, TValueType>(FieldInfo fieldInfo)
        {
            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");
            var valueParameter = Expression.Parameter(typeof(TValueType), "value");

            var field = GetField(fieldInfo, typeof(TInstanceType), instanceParameter);

            var valueCast = valueParameter.Coerce(typeof(TValueType), fieldInfo.FieldType);
            var assignValue = Expression.Assign(field, valueCast);

            var expression = Expression.Lambda<Action<TInstanceType, TValueType>>(
                assignValue,
                instanceParameter,
                valueParameter);
            return expression.Compile();
        }

        public static Action<object> CreateStaticSetValueAction(FieldInfo fieldInfo)
        {
            return CreateStaticSetValueAction<object>(fieldInfo);
        }

        public static Action<TValueType> CreateStaticSetValueAction<TValueType>(FieldInfo fieldInfo)
        {
            var valueParameter = Expression.Parameter(typeof(TValueType), "value");
            var valueCast = valueParameter.Coerce(typeof(TValueType), fieldInfo.FieldType);

            var field = Expression.Field(null, fieldInfo);
            var assignValue = Expression.Assign(field, valueCast);

            var expression = Expression.Lambda<Action<TValueType>>(assignValue, valueParameter);
            return expression.Compile();
        }

        private static MemberExpression GetField(FieldInfo fieldInfo, Type declaringType, ParameterExpression instanceParameter)
        {
            MemberExpression field;

            if (fieldInfo.IsStatic)
            {
                field = Expression.Field(null, fieldInfo);
            }
            else
            {
                var instanceCast = instanceParameter.Coerce(declaringType, fieldInfo.DeclaringType);
                field = Expression.Field(instanceCast, fieldInfo);
            }

            return field;
        }

        #endregion

        #region Property

        public static Func<object, object> CreateGetValueFunc(PropertyInfo propertyInfo)
        {
            return CreateGetValueFunc<object, object>(propertyInfo);
        }

        public static Func<TInstanceType, TReturnType> CreateGetValueFunc<TInstanceType, TReturnType>(PropertyInfo propertyInfo)
        {
            var method = GetPropertyAccessorMethod(propertyInfo, p => p.GetGetMethod(true), p => p.CanRead, "read from");

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            MethodCallExpression call;

            if (propertyInfo.IsStatic())
            {
                call = Expression.Call(method);
            }
            else
            {
                var instanceCast = instanceParameter.Coerce(typeof(TInstanceType), method.DeclaringType);
                call = Expression.Call(instanceCast, method);
            }

            var body = call.Coerce(method.ReturnType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TInstanceType, TReturnType>>(
                body,
                instanceParameter);

            return expression.Compile();
        }

        public static Func<object> CreateStaticGetValueFunc(PropertyInfo propertyInfo)
        {
            return CreateStaticGetValueFunc<object>(propertyInfo);
        }

        public static Func<TReturnType> CreateStaticGetValueFunc<TReturnType>(PropertyInfo propertyInfo)
        {
            var method = GetPropertyAccessorMethod(propertyInfo, p => p.GetGetMethod(true), p => p.CanRead, "read from");

            var call = Expression.Call(method);
            var body = call.Coerce(method.ReturnType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TReturnType>>(body);
            return expression.Compile();
        }

        public static Action<object, object> CreateSetValueFunc(PropertyInfo propertyInfo)
        {
            return CreateSetValueFunc<object, object>(propertyInfo);
        }

        public static Action<TInstanceType, TValueType> CreateSetValueFunc<TInstanceType, TValueType>(PropertyInfo propertyInfo)
        {
            var method = GetPropertyAccessorMethod(propertyInfo, p => p.GetSetMethod(true), p => p.CanWrite, "write to");

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");
            var valueParameter = Expression.Parameter(typeof(TValueType), "value");

            var valueCast = valueParameter.Coerce(typeof(TValueType), method.GetParameters().Single().ParameterType);

            MethodCallExpression call;

            if (propertyInfo.IsStatic())
            {
                call = Expression.Call(method, valueCast);
            }
            else
            {
                var instanceCast = instanceParameter.Coerce(typeof(TInstanceType), method.DeclaringType);
                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                call = Expression.Call(instanceCast, method, valueCast);
            }

            var expression = Expression.Lambda<Action<TInstanceType, TValueType>>(
                call,
                instanceParameter,
                valueParameter);

            return expression.Compile();
        }

        public static Action<object> CreateStaticSetValueAction(PropertyInfo propertyInfo)
        {
            return CreateStaticSetValueAction<object>(propertyInfo);
        }

        public static Action<TValueType> CreateStaticSetValueAction<TValueType>(PropertyInfo propertyInfo)
        {
            var method = GetPropertyAccessorMethod(propertyInfo, p => p.GetSetMethod(true), p => p.CanWrite, "write to");

            var valueParameter = Expression.Parameter(typeof(TValueType), "value");
            var valueCast = valueParameter.Coerce(typeof(TValueType), method.GetParameters().Single().ParameterType);

            var call = Expression.Call(method, valueCast);
            var expression = Expression.Lambda<Action<TValueType>>(call, valueParameter);

            return expression.Compile();
        }

        private static MethodInfo GetPropertyAccessorMethod(PropertyInfo propertyInfo, Func<PropertyInfo, MethodInfo> getAccessor, Func<PropertyInfo, bool> canUseAccessor, string accessVerbPhrase)
        {
            if (!canUseAccessor(propertyInfo))
            {
                // ReSharper disable PossibleNullReferenceException
                throw new MemberAccessException(string.Format("Cannot {0} property: {1}.{2}",
                    accessVerbPhrase, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
            }

            var method = getAccessor(propertyInfo);

            if (method == null)
            {
                throw new MemberAccessException(string.Format("Cannot {0} property: {1}.{2}",
                    accessVerbPhrase, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
                    // ReSharper restore PossibleNullReferenceException
            }

            return method;
        }

        #endregion

        #region Contructor

        public static Func<object[], object> CreateConstructorFunc(ConstructorInfo ctor)
        {
            if (ctor == null)
            {
                throw new ArgumentNullException("ctor");
            }

            var ctorParameters = ctor.GetParameters();

            var parameter = Expression.Parameter(typeof(object[]));
            var coercedParameters =
                ctorParameters.Select(
                    (parameterInfo, i) =>
                        Expression.ArrayAccess(parameter, Expression.Constant(i)).Coerce(typeof(object), parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof (object));

            var expression = Expression.Lambda<Func<object[], object>>(newCast, new[] { parameter });
            return expression.Compile();
        }

        #endregion

        #region Method

        public static Func<object, object[], object> CreateNonGenericInstanceMethodFunc(MethodInfo methodInfo)
        {
            var methodInfoParameters = methodInfo.GetParameters();

            var instanceParameter = Expression.Parameter(typeof(object), "instance");

            var parameter = Expression.Parameter(typeof(object[]), "args");
            var call = GetNonGenericCallExpression(methodInfo, instanceParameter, methodInfoParameters, parameter);

            var expression = Expression.Lambda<Func<object, object[], object>>(call, new [] { instanceParameter, parameter });
            return expression.Compile();
        }

        public static Action<object, object[]> CreateNonGenericInstanceMethodAction(MethodInfo methodInfo)
        {
            var methodInfoParameters = methodInfo.GetParameters();

            var instanceParameter = Expression.Parameter(typeof(object), "instance");

            var parameter = Expression.Parameter(typeof(object[]), "args");
            var call = GetNonGenericCallExpression(methodInfo, instanceParameter, methodInfoParameters, parameter);

            var expression = Expression.Lambda<Action<object, object[]>>(call, new[] { instanceParameter, parameter });
            return expression.Compile();
        }

        public static Func<object[], object> CreateNonGenericStaticMethodFunc(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = methodInfo.GetParameters();

            var parameter = Expression.Parameter(typeof(object[]), "args");
            var call = GetNonGenericCallExpression(methodInfo, null, methodInfoParameters, parameter);

            var expression = Expression.Lambda<Func<object[], object>>(call, parameter);
            return expression.Compile();
        }

        public static Action<object[]> CreateNonGenericStaticMethodAction(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = methodInfo.GetParameters();

            var parameter = Expression.Parameter(typeof(object[]), "args");
            var call = GetNonGenericCallExpression(methodInfo, null, methodInfoParameters, parameter);

            var expression = Expression.Lambda<Action<object[]>>(call, parameter);
            return expression.Compile();
        }

        #endregion

        private static Expression Coerce(this Expression sourceExpression, Type sourceType, Type targetType)
        {
            if (!targetType.IsAssignableFrom(sourceType) || (targetType == typeof(object) && sourceType.IsValueType))
            {
                return Expression.Convert(sourceExpression, targetType);
            }

            return sourceExpression;
        }

        private static Expression GetCallExpression(
            MethodInfo methodInfo,
            ParameterExpression instanceParameter,
            Type instanceType,
            Type returnType,
            IEnumerable<ParameterInfo> parameterInfos,
            IEnumerable<Expression> parameterExpressions)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            returnType = returnType ?? typeof(void);
            parameterInfos = parameterInfos ?? Enumerable.Empty<ParameterInfo>();
            parameterExpressions = parameterExpressions ?? Enumerable.Empty<ParameterExpression>();

            var coercedParameters = parameterExpressions.Zip(
                parameterInfos,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            MethodCallExpression call;

            if (methodInfo.IsStatic)
            {
                call = Expression.Call(methodInfo, coercedParameters);
            }
            else
            {
                if (instanceParameter == null)
                {
                    throw new ArgumentNullException("instanceParameter", "'instanceParameter' cannot be null when 'methodInfo' is not static.");
                }

                if (instanceType == null)
                {
                    throw new ArgumentNullException("instanceType", "'instanceType' cannot be null when 'methodInfo' is not static.");
                }

                var instanceCast = instanceParameter.Coerce(instanceType, methodInfo.DeclaringType);
                call = Expression.Call(instanceCast, methodInfo, coercedParameters);
            }

            var callCast = call.Coerce(methodInfo.ReturnType, returnType);
            return callCast;
        }

        private static Expression GetNonGenericCallExpression(
            MethodInfo methodInfo,
            ParameterExpression instanceParameter,
            IEnumerable<ParameterInfo> parameterInfos,
            ParameterExpression parameter)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            parameterInfos = parameterInfos ?? Enumerable.Empty<ParameterInfo>();

            var coercedParameters =
                parameterInfos.Select(
                    (parameterInfo, i) =>
                        Expression.ArrayAccess(parameter, Expression.Constant(i)).Coerce(typeof(object), parameterInfo.ParameterType));

            MethodCallExpression call;

            if (methodInfo.IsStatic)
            {
                call = Expression.Call(methodInfo, coercedParameters);
            }
            else
            {
                if (instanceParameter == null)
                {
                    throw new ArgumentNullException("instanceParameter", "'instanceParameter' cannot be null when 'methodInfo' is not static.");
                }

                var instanceCast = instanceParameter.Coerce(typeof(object), methodInfo.DeclaringType);
                call = Expression.Call(instanceCast, methodInfo, coercedParameters);
            }

            var callCast = call.Coerce(methodInfo.ReturnType, typeof(object));
            return callCast;
        }

        private static ParameterInfo[] GetMethodInfoParameters(MethodInfo methodInfo, params Type[] parameterTypes)
        {
            var methodInfoParameters = methodInfo.GetParameters();

            if (methodInfoParameters.Length != parameterTypes.Length)
            {
                throw new ArgumentException(string.Format("Wrong number of parameters. Should be {0}, but was {1}.", parameterTypes.Length, methodInfoParameters.Length), "methodInfo");
            }

            // TODO: make sure that methodInfoParameters is compatible with parameterTypes.

            return methodInfoParameters;
        }
    }
}