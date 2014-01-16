using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectinator
{
    public static partial class FuncFactory
    {
        public static Func<TReturnType> CreateConstructorFunc<TReturnType>(ConstructorInfo ctor = null)
        {
            if (ctor == null)
            {
                ctor = typeof(TReturnType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {  }, null);

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

            var expression = Expression.Lambda<Func<TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TReturnType> CreateConstructorFunc<TReturnType, TArg1>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType> CreateConstructorFunc<TReturnType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(ConstructorInfo ctor = null)
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

            var expression = Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TReturnType>>(newCast, parameters);
            return expression.Compile();
        }

    }
}