﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public static partial class ExpressionFactory
    {
        #region CreateConstructorFunc

        public static Expression<Func<TReturnType>> CreateConstructorFuncExpression<TReturnType>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {}, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ()"));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 0)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 0, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] {  };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0})", typeof(TArg1).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 1)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 1, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1})", typeof(TArg1).Name, typeof(TArg2).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 2)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 2, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 3)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 3, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 4)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 4, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 5)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 5, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 6)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 6, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 7)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 7, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 8)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 8, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 9)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 9, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 10)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 10, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name, typeof(TArg11).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 11)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 11, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name, typeof(TArg11).Name, typeof(TArg12).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 12)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 12, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name, typeof(TArg11).Name, typeof(TArg12).Name, typeof(TArg13).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 13)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 13, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name, typeof(TArg11).Name, typeof(TArg12).Name, typeof(TArg13).Name, typeof(TArg14).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 14)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 14, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name, typeof(TArg11).Name, typeof(TArg12).Name, typeof(TArg13).Name, typeof(TArg14).Name, typeof(TArg15).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 15)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 15, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType>> CreateConstructorFuncExpression<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15), typeof(TArg16) }, null);

                if (ctor == null)
                {
                    throw new MissingMemberException(string.Format("Unable to find a constructor with the following types: ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})", typeof(TArg1).Name, typeof(TArg2).Name, typeof(TArg3).Name, typeof(TArg4).Name, typeof(TArg5).Name, typeof(TArg6).Name, typeof(TArg7).Name, typeof(TArg8).Name, typeof(TArg9).Name, typeof(TArg10).Name, typeof(TArg11).Name, typeof(TArg12).Name, typeof(TArg13).Name, typeof(TArg14).Name, typeof(TArg15).Name, typeof(TArg16).Name));
                }
            }

            var ctorParameters = ctor.GetParameters();

            if (ctorParameters.Length != 16)
            {
                throw new ArgumentException(string.Format("ConstructorInfo has wrong number of parameters. Should be 16, but was {0}.", ctorParameters.Length), "ctor");
            }

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15"), Expression.Parameter(typeof(TArg16), "arg16") };
            var coercedParameters = parameters.Zip(
                ctorParameters,
                (parameterExpression, parameterInfo) =>
                    parameterExpression.Coerce(parameterExpression.Type, parameterInfo.ParameterType));

            var newExpression = Expression.New(ctor, coercedParameters);
            var newCast = newExpression.Coerce(ctor.DeclaringType, typeof(TReturnType));

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType>>(newCast, string.Format("{0}.ctor", ctor.DeclaringType.Name), parameters);
            return expression;
        }

        #endregion

        #region CreateInstanceMethodFunc

        public static Expression<Func<TInstanceType, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo);

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] {};
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>> CreateInstanceMethodFuncExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        #endregion

        #region CreateInstanceMethodAction

        public static Expression<Action<TInstanceType>> CreateInstanceMethodActionExpression<TInstanceType>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo);

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] {};
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1>> CreateInstanceMethodActionExpression<TInstanceType, TArg1>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        public static Expression<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>> CreateInstanceMethodActionExpression<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(MethodInfo methodInfo)
        {
            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15));

            var instanceParameter = Expression.Parameter(typeof(TInstanceType), "instance");

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15") };
            var call = GetCallExpression(methodInfo, instanceParameter, typeof(TInstanceType), null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TInstanceType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), instanceParameter.Then(parameters));
            return expression;
        }

        #endregion

        #region CreateStaticMethodFunc

        public static Expression<Func<TReturnType>> CreateStaticMethodFuncExpression<TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo);

            var parameters = new ParameterExpression[] {};
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType>> CreateStaticMethodFuncExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15), typeof(TArg16));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15"), Expression.Parameter(typeof(TArg16), "arg16") };
            var call = GetCallExpression(methodInfo, null, null, typeof(TReturnType), methodInfoParameters, parameters);

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        #endregion

        #region CreateStaticMethodAction

        public static Expression<Action> CreateStaticMethodActionExpression(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo);

            var parameters = new ParameterExpression[] {};
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1>> CreateStaticMethodActionExpression<TArg1>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2>> CreateStaticMethodActionExpression<TArg1, TArg2>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        public static Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>> CreateStaticMethodActionExpression<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(MethodInfo methodInfo)
        {
            if (!methodInfo.IsStatic)
            {
                throw new ArgumentException("Cannot create static method func on an instance MethodInfo", "methodInfo");
            }

            var methodInfoParameters = GetMethodInfoParameters(methodInfo, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4), typeof(TArg5), typeof(TArg6), typeof(TArg7), typeof(TArg8), typeof(TArg9), typeof(TArg10), typeof(TArg11), typeof(TArg12), typeof(TArg13), typeof(TArg14), typeof(TArg15), typeof(TArg16));

            var parameters = new ParameterExpression[] { Expression.Parameter(typeof(TArg1), "arg1"), Expression.Parameter(typeof(TArg2), "arg2"), Expression.Parameter(typeof(TArg3), "arg3"), Expression.Parameter(typeof(TArg4), "arg4"), Expression.Parameter(typeof(TArg5), "arg5"), Expression.Parameter(typeof(TArg6), "arg6"), Expression.Parameter(typeof(TArg7), "arg7"), Expression.Parameter(typeof(TArg8), "arg8"), Expression.Parameter(typeof(TArg9), "arg9"), Expression.Parameter(typeof(TArg10), "arg10"), Expression.Parameter(typeof(TArg11), "arg11"), Expression.Parameter(typeof(TArg12), "arg12"), Expression.Parameter(typeof(TArg13), "arg13"), Expression.Parameter(typeof(TArg14), "arg14"), Expression.Parameter(typeof(TArg15), "arg15"), Expression.Parameter(typeof(TArg16), "arg16") };
            var call = GetCallExpression(methodInfo, null, null, null, methodInfoParameters, parameters);

            var expression = Expression.Lambda<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>>(call, string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name), parameters);
            return expression;
        }

        #endregion
    }
}