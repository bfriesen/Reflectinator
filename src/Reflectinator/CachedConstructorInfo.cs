using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Reflectinator
{
    public abstract class CachedConstructorInfo : DynamicObject, ICachedConstructorInfo
    {
        private readonly Func<object[], object> _invoke;
        private readonly ConstructorInfo _constructorInfo;
        private readonly ICachedType _declaringType;
        private readonly ICachedType[] _parameters;

        protected CachedConstructorInfo(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
            {
                throw new ArgumentNullException("constructorInfo");
            }

            _invoke = (Func<object[], object>)FuncFactory.CreateConstructorFunc(constructorInfo, false);
            _constructorInfo = constructorInfo;
            _declaringType = CachedType.Create(constructorInfo.DeclaringType);
            _parameters = constructorInfo.GetParameters().Select(p => CachedType.Create(p.ParameterType)).ToArray();
        }

        public ConstructorInfo ConstructorInfo { get { return _constructorInfo; } }
        public bool IsPublic { get { return _constructorInfo.IsPublic; } }
        public ICachedType DeclaringType { get { return _declaringType; } }
        public ICachedType[] Parameters { get { return _parameters; } }

        public object Invoke(params object[] args)
        {
            return _invoke(args);
        }
    }
}