using System;
using System.Dynamic;

namespace Reflectinator
{
    public class Constructor<TDeclaringType, TArg1> : Constructor
    {
        private readonly Lazy<Func<TArg1, TDeclaringType>> _invoke;

        public Constructor()
            : base(typeof(TDeclaringType).GetConstructorInfo(typeof(TArg1)))
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