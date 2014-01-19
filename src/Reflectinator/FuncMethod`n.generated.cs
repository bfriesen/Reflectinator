﻿////////////////////////////////////////////////////////////////////////////////
// This file was generated by a tool. Any manual changes to this file will be //
// lost if/when the file is regenerated. If changes need to be made to this   //
// file, they should be made in FuncMethod`n.tt, which regenerates this file. //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;

namespace Reflectinator
{
    public sealed class FuncMethod<TDeclaringType, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance) { return _invoke.Value(instance); }
        Func<TDeclaringType, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1) { return _invoke.Value(instance, arg1); }
        Func<TDeclaringType, TArg1, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2) { return _invoke.Value(instance, arg1, arg2); }
        Func<TDeclaringType, TArg1, TArg2, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3) { return _invoke.Value(instance, arg1, arg2, arg3); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) { return _invoke.Value(instance, arg1, arg2, arg3, arg4); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

    public sealed class FuncMethod<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType> : Method, IFuncMethod
    {
        private readonly Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>> _invoke;

        internal FuncMethod(MethodInfo methodInfo)
            : base(methodInfo)
        {
            _invoke = new Lazy<Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>>(() => FuncFactory.CreateInstanceMethodFunc<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType>(methodInfo));
        }
    
        public TReturnType Invoke(TDeclaringType instance, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15) { return _invoke.Value(instance, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15); }
        Func<TDeclaringType, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TReturnType> InvokeDelegate { get { return _invoke.Value; } }
    }

}