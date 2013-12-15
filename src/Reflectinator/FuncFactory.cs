using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public static class FuncFactory
    {
        internal static Delegate CreateConstructorFunc(ConstructorInfo ctor, bool stronglyTyped)
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