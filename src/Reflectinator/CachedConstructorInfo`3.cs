using System;
using System.Dynamic;

namespace Reflectinator
{
    public class CachedConstructorInfo<TDeclaringType, TArg1, TArg2> : CachedConstructorInfo
    {
        private readonly Func<TArg1, TArg2, TDeclaringType> _invoke;

        public CachedConstructorInfo()
            : base(typeof(TDeclaringType).GetConstructor(new[] { typeof(TArg1), typeof(TArg2) }))
        {
            _invoke = (Func<TArg1, TArg2, TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true);
        }

        public TDeclaringType Invoke(TArg1 arg1, TArg2 arg2)
        {
            return _invoke(arg1, arg2);
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = null;

            if (args.Length != 2)
            {
                return false;
            }

            try
            {
                result = Invoke((TArg1)args[0], (TArg2)args[1]);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}