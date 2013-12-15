using System;
using System.Dynamic;

namespace Reflectinator
{
    public class CachedConstructorInfo<TDeclaringType> : CachedConstructorInfo
    {
        private readonly Func<TDeclaringType> _invoke;

        public CachedConstructorInfo()
            : base(typeof(TDeclaringType).GetConstructor(Type.EmptyTypes))
        {
            _invoke = (Func<TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true);
        }

        public TDeclaringType Invoke()
        {
            return _invoke();
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = null;

            if (args.Length != 0)
            {
                return false;
            }

            try
            {
                result = Invoke();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}