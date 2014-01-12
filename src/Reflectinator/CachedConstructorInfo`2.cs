using System;
using System.Dynamic;

namespace Reflectinator
{
    public class CachedConstructorInfo<TDeclaringType, TArg1> : CachedConstructorInfo
    {
        private readonly Lazy<Func<TArg1, TDeclaringType>> _invoke;

        public CachedConstructorInfo()
            : base(typeof(TDeclaringType).GetConstructor(new[] { typeof(TArg1) }))
        {
            _invoke = new Lazy<Func<TArg1, TDeclaringType>>(() => (Func<TArg1, TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true));
        }

        public TDeclaringType Invoke(TArg1 arg1)
        {
            return _invoke.Value(arg1);
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = null;

            if (args.Length != 1)
            {
                return false;
            }

            try
            {
                result = Invoke((TArg1)args[0]);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}