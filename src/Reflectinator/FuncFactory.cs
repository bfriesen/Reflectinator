using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    internal static class FuncFactory
    {
        public static Func<object, object> CreateGetValueFunc(PropertyInfo propertyInfo)
        {
            return CreateGetValueFunc<object, object>(propertyInfo, isStronglyTyped:false);
        }

        public static Action<object, object> CreateSetValueFunc(PropertyInfo propertyInfo)
        {
            return CreateSetValueFunc<object, object>(propertyInfo, isStronglyTyped:false);
        }

        public static Func<TDeclaringType, TPropertyType> CreateGetValueFunc<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return CreateGetValueFunc<TDeclaringType, TPropertyType>(propertyInfo, isStronglyTyped:true);
        }

        public static Action<TDeclaringType, TPropertyType> CreateSetValueFunc<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo)
        {
            return CreateSetValueFunc<TDeclaringType, TPropertyType>(propertyInfo, isStronglyTyped:true);
        }

        private static Func<TDeclaringType, TPropertyType> CreateGetValueFunc<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo, bool isStronglyTyped)
        {
            var method = GetPropertyAccessorMethod(propertyInfo, p => p.GetGetMethod(true), p => p.CanRead, "read from");

            var instanceParameter = Expression.Parameter(typeof(TDeclaringType), "instance");

            MethodCallExpression call;

            if (propertyInfo.IsStatic())
            {
                call = Expression.Call(method);
            }
            else if (isStronglyTyped)
            {
                call = Expression.Call(instanceParameter, method);
            }
            else
            {
                var instanceCast =
                    method.DeclaringType.IsValueType
                        ? Expression.Convert(instanceParameter, method.DeclaringType)
                        : Expression.TypeAs(instanceParameter, method.DeclaringType);

                call = Expression.Call(instanceCast, method);
            }

            var expression = Expression.Lambda<Func<TDeclaringType, TPropertyType>>(
                call,
                instanceParameter);

            return expression.Compile();
        }

        private static Action<TDeclaringType, TPropertyType> CreateSetValueFunc<TDeclaringType, TPropertyType>(PropertyInfo propertyInfo, bool isStronglyTyped)
        {
            var method = GetPropertyAccessorMethod(propertyInfo, p => p.GetSetMethod(true), p => p.CanWrite, "write to");

            var instanceParameter = Expression.Parameter(typeof(TDeclaringType), "instance");
            var valueParameter = Expression.Parameter(typeof(TPropertyType), "value");

            MethodCallExpression call;

            if (isStronglyTyped)
            {
                if (propertyInfo.IsStatic())
                {
                    call = Expression.Call(method, valueParameter);
                }
                else
                {
                    call = Expression.Call(instanceParameter, method, valueParameter);
                }
            }
            else
            {
                var valueParameterType = method.GetParameters().Single().ParameterType;
                var valueCast =
                    valueParameterType.IsValueType
                        ? Expression.Convert(valueParameter, valueParameterType)
                        : Expression.TypeAs(valueParameter, valueParameterType);

                if (propertyInfo.IsStatic())
                {
                    call = Expression.Call(method, valueCast);
                }
                else
                {
                    var instanceCast =
                        method.DeclaringType.IsValueType
                            ? Expression.Convert(instanceParameter, method.DeclaringType)
                            : Expression.TypeAs(instanceParameter, method.DeclaringType);

                    call = Expression.Call(instanceCast, method, valueCast);
                }
            }

            var expression = Expression.Lambda<Action<TDeclaringType, TPropertyType>>(
                call,
                instanceParameter,
                valueParameter);

            return expression.Compile();
        }

        private static MethodInfo GetPropertyAccessorMethod(PropertyInfo propertyInfo, Func<PropertyInfo, MethodInfo> getAccessor, Func<PropertyInfo, bool> canUseAccessor, string accessVerbPhrase)
        {
            if (!canUseAccessor(propertyInfo))
            {
                throw new MemberAccessException(string.Format("Cannot {0} property: {1}.{2}",
                    accessVerbPhrase, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
            }

            var method = getAccessor(propertyInfo);

            if (method == null)
            {
                throw new MemberAccessException(string.Format("Cannot {0} property: {1}.{2}",
                    accessVerbPhrase, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
            }

            return method;
        }

        public static Delegate CreateConstructorFunc(ConstructorInfo ctor, bool stronglyTyped)
        {
            if (ctor == null)
            {
                throw new ArgumentNullException("ctor");
            }

            NewExpression expressionBody;
            ParameterExpression[] parameters;
            Type returnType;

            if (stronglyTyped)
            {
                parameters = ctor.GetParameters().Select(p => Expression.Parameter(p.ParameterType)).ToArray();
                expressionBody = Expression.New(ctor, parameters.Cast<Expression>());
                returnType = expressionBody.Type;
            }
            else
            {
                parameters = new[] { Expression.Parameter(typeof(object[])) };

                var ctorArgs =
                    ctor.GetParameters().Select((ctorParameter, index) => new { ctorParameter, lambdaParameter = Expression.ArrayAccess(parameters[0], Expression.Constant(index)) })
                                           .Select(x =>
                                                   x.ctorParameter.ParameterType.IsValueType
                                                       ? (Expression)Expression.Convert(x.lambdaParameter, x.ctorParameter.ParameterType)
                                                       : Expression.TypeAs(x.lambdaParameter, x.ctorParameter.ParameterType)).ToArray();
                expressionBody = Expression.New(ctor, ctorArgs);

                returnType = typeof(object);
            }

            var expression = GetLambdaExpressionForFunc(expressionBody, parameters, returnType);
            return expression.Compile();
        }

        private static LambdaExpression GetLambdaExpressionForFunc(Expression expression, ParameterExpression[] parameters, Type returnType)
        {
            switch (parameters.Length)
            {
                case 0:
                    return
                        Expression.Lambda(
                            typeof(Func<>).MakeGenericType(
                                returnType),
                            expression);
                case 1:
                    return
                        Expression.Lambda(
                            typeof(Func<,>).MakeGenericType(
                                parameters[0].Type,
                                returnType),
                            expression,
                            parameters);
                case 2:
                    return
                        Expression.Lambda(
                            typeof(Func<,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                returnType),
                            expression,
                            parameters);
                case 3:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                returnType),
                            expression,
                            parameters);
                case 4:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                returnType),
                            expression,
                            parameters);
                case 5:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                returnType),
                            expression,
                            parameters);
                case 6:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                returnType),
                            expression,
                            parameters);
                case 7:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                returnType),
                            expression,
                            parameters);
                case 8:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                returnType),
                            expression,
                            parameters);
                case 9:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                returnType),
                            expression,
                            parameters);
                case 10:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                returnType),
                            expression,
                            parameters);
                case 11:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                parameters[10].Type,
                                returnType),
                            expression,
                            parameters);
                case 12:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                parameters[10].Type,
                                parameters[11].Type,
                                returnType),
                            expression,
                            parameters);
                case 13:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                parameters[10].Type,
                                parameters[11].Type,
                                parameters[12].Type,
                                returnType),
                            expression,
                            parameters);
                case 14:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                parameters[10].Type,
                                parameters[11].Type,
                                parameters[12].Type,
                                parameters[13].Type,
                                returnType),
                            expression,
                            parameters);
                case 15:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                parameters[10].Type,
                                parameters[11].Type,
                                parameters[12].Type,
                                parameters[13].Type,
                                parameters[14].Type,
                                returnType),
                            expression,
                            parameters);
                case 16:
                    return
                        Expression.Lambda(
                            typeof(Func<,,,,,,,,,,,,,,,,>).MakeGenericType(
                                parameters[0].Type,
                                parameters[1].Type,
                                parameters[2].Type,
                                parameters[3].Type,
                                parameters[4].Type,
                                parameters[5].Type,
                                parameters[6].Type,
                                parameters[7].Type,
                                parameters[8].Type,
                                parameters[9].Type,
                                parameters[10].Type,
                                parameters[11].Type,
                                parameters[12].Type,
                                parameters[13].Type,
                                parameters[14].Type,
                                parameters[15].Type,
                                returnType),
                            expression,
                            parameters);
                default:
                    throw new InvalidOperationException("Too many constructor parameters.");
            }
        }
    }
}