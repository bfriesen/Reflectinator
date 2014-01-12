using System;
using System.Collections.Concurrent;

namespace Reflectinator
{
    public static class TypeInfo
    {
        private static readonly ConcurrentDictionary<Type, Func<ITypeInfo>> _createTypeInfoMap = new ConcurrentDictionary<Type, Func<ITypeInfo>>();

        public static ITypeInfo Create(Type type)
        {
            var createTypeInfo = _createTypeInfoMap.GetOrAdd(
                type,
                t => FuncFactory.CreateDefaultConstructorFunc<ITypeInfo>(typeof(TypeInfo<>).MakeGenericType(t).GetConstructorInfo()));
            return createTypeInfo();
        }

        public static TypeInfo<T> Create<T>()
        {
            var createTypeInfo = _createTypeInfoMap.GetOrAdd(
                typeof(T),
                t => FuncFactory.CreateDefaultConstructorFunc<ITypeInfo>(typeof(TypeInfo<T>).GetConstructorInfo()));
            return (TypeInfo<T>)createTypeInfo();
        }
    }
}