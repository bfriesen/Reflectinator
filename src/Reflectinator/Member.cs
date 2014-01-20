using System;
using System.Reflection;

namespace Reflectinator
{
    public abstract class Member : IMember
    {
        private readonly Lazy<ITypeCrawler> _declaringType;

        internal Member(MemberInfo memberInfo)
        {
            _declaringType = new Lazy<ITypeCrawler>(() => TypeCrawler.Get(memberInfo.DeclaringType));
        }

        public ITypeCrawler DeclaringType
        {
            get { return _declaringType.Value; }
        }

        public abstract string Name { get; }
        public abstract bool IsPublic { get; }
        public abstract bool IsStatic { get; }
    }
}