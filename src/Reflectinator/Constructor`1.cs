using System;
using System.Dynamic;
using System.Reflection;

namespace Reflectinator
{
    public sealed class Constructor<TDeclaringType> : Constructor
    {
        private readonly Lazy<Func<TDeclaringType>> _invoke;

        internal Constructor()
            : this(typeof(TDeclaringType).GetConstructorInfo())
        {
        }

        internal Constructor(ConstructorInfo constructorInfo)
            : base(constructorInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType>>(() => (Func<TDeclaringType>)FuncFactory.CreateConstructorFunc(ConstructorInfo, true));
        }

        public TDeclaringType Invoke()
        {
            return _invoke.Value();
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