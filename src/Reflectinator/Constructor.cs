using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Reflectinator
{
    public class Constructor : DynamicObject, IConstructor
    {
        private static readonly ConcurrentDictionary<int, IConstructor> _constructorsMap = new ConcurrentDictionary<int, IConstructor>();

        private readonly ConstructorInfo _constructorInfo;
        private readonly Lazy<Func<object[], object>> _invoke;
        private readonly Lazy<ITypeCrawler> _declaringType;
        private readonly Lazy<ITypeCrawler[]> _parameters;

        internal Constructor(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
            {
                throw new ArgumentNullException("constructorInfo");
            }

            _constructorInfo = constructorInfo;
            _invoke = new Lazy<Func<object[], object>>(() => (Func<object[], object>)FuncFactory.CreateConstructorFunc(constructorInfo, false));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(constructorInfo.DeclaringType));
            _parameters = new Lazy<ITypeCrawler[]>(() => constructorInfo.GetParameters().Select(p => TypeCrawler.Get(p.ParameterType)).ToArray());
        }

        public static IConstructor Get(ConstructorInfo constructorInfo)
        {
            var parameters = constructorInfo.GetParameters().Select(p => p.ParameterType).ToArray();

            return _constructorsMap.GetOrAdd(
                GetKey(constructorInfo.DeclaringType, parameters),
                key =>
                {
                    Type constructorType;

                    switch (parameters.Length)
                    {
                        case 0:
                            constructorType = typeof(Constructor<>).MakeGenericType(constructorInfo.DeclaringType);
                            break;
                        case 1:
                            constructorType = typeof(Constructor<,>).MakeGenericType(constructorInfo.DeclaringType, parameters[0]);
                            break;
                        case 2:
                            constructorType = typeof(Constructor<,,>).MakeGenericType(constructorInfo.DeclaringType, parameters[0], parameters[1]);
                            break;
                        default: // TODO: Implement the rest of the generic implementations of Constructor.
                            throw new NotImplementedException();
                    }

                    return (IConstructor)Activator.CreateInstance(constructorType, BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { constructorInfo }, null);
                });
        }

        public static Constructor<TDeclaringType> Get<TDeclaringType>()
        {
            return (Constructor<TDeclaringType>)_constructorsMap.GetOrAdd(
                GetKey(typeof(TDeclaringType)),
                key => new Constructor<TDeclaringType>());
        }

        public static Constructor<TDeclaringType, TArg1> Get<TDeclaringType, TArg1>()
        {
            return (Constructor<TDeclaringType, TArg1>)_constructorsMap.GetOrAdd(
                GetKey(typeof(TDeclaringType), typeof(TArg1)),
                key => new Constructor<TDeclaringType, TArg1>());
        }

        public static Constructor<TDeclaringType, TArg1, TArg2> Get<TDeclaringType, TArg1, TArg2>()
        {
            return (Constructor<TDeclaringType, TArg1, TArg2>)_constructorsMap.GetOrAdd(
                GetKey(typeof(TDeclaringType), typeof(TArg1), typeof(TArg2)),
                key => new Constructor<TDeclaringType, TArg1, TArg2>());
        }

        private static int GetKey(Type declaringType, params Type[] argTypes)
        {
            unchecked
            {
                return argTypes.Aggregate(declaringType.GetHashCode(), (current, argType) => (current * 397) ^ argType.GetHashCode());
            }
        }

        public ConstructorInfo ConstructorInfo { get { return _constructorInfo; } }
        public bool IsPublic { get { return _constructorInfo.IsPublic; } }
        public ITypeCrawler DeclaringType { get { return _declaringType.Value; } }
        public ITypeCrawler[] Parameters { get { return _parameters.Value; } }

        public object Invoke(params object[] args)
        {
            return _invoke.Value(args);
        }
    }
}