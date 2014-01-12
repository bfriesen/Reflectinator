using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Reflectinator
{
    public class Constructor : DynamicObject, IConstructor
    {
        private readonly ConstructorInfo _constructorInfo;
        private readonly Lazy<Func<object[], object>> _invoke;
        private readonly Lazy<ITypeCrawler> _declaringType;
        private readonly Lazy<ITypeCrawler[]> _parameters;

        protected Constructor(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
            {
                throw new ArgumentNullException("constructorInfo");
            }

            _constructorInfo = constructorInfo;
            _invoke = new Lazy<Func<object[], object>>(() => (Func<object[], object>)FuncFactory.CreateConstructorFunc(constructorInfo, false));
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Create(constructorInfo.DeclaringType));
            _parameters = new Lazy<ITypeCrawler[]>(() => constructorInfo.GetParameters().Select(p => TypeCrawler.Create(p.ParameterType)).ToArray());
        }

        public static IConstructor Create(ConstructorInfo constructorInfo)
        {
            return new Constructor(constructorInfo);
        }

        public static Constructor<TDeclaringType> Create<TDeclaringType>()
        {
            return new Constructor<TDeclaringType>();
        }

        public static Constructor<TDeclaringType, TArg1> Create<TDeclaringType, TArg1>()
        {
            return new Constructor<TDeclaringType, TArg1>();
        }

        public static Constructor<TDeclaringType, TArg1, TArg2> Create<TDeclaringType, TArg1, TArg2>()
        {
            return new Constructor<TDeclaringType, TArg1, TArg2>();
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