using NUnit.Framework;

namespace Reflectinator.Tests
{
// ReSharper disable UnusedParameter.Local
    public class CachedConstructorInfoTests
    {
        private readonly string _s;
        private readonly int _i;

        public CachedConstructorInfoTests()
        {
        }

        public CachedConstructorInfoTests(string s)
        {
            _s = s;
        }

        public CachedConstructorInfoTests(int i)
        {
            _i = i;
        }

        public CachedConstructorInfoTests(string s, int i)
        {
            _s = s;
            _i = i;
        }

        [Test]
        public void CanSpecifyConstructorWithGenericArguments()
        {
            Assert.That(() => new CachedConstructorInfo<CachedConstructorInfoTests>(), Throws.Nothing);
            Assert.That(() => new CachedConstructorInfo<CachedConstructorInfoTests, string>(), Throws.Nothing);
            Assert.That(() => new CachedConstructorInfo<CachedConstructorInfoTests, int>(), Throws.Nothing);
            Assert.That(() => new CachedConstructorInfo<CachedConstructorInfoTests, string, int>(), Throws.Nothing);
        }

        [Test]
        public void InvalidConstructorTypesThrowsException()
        {
            Assert.That(() => new CachedConstructorInfo<CachedConstructorInfoTests, decimal>(), Throws.Exception);
        }

        [Test]
        public void CanInvokeWithObjectArrayAsArgs()
        {
            var ctor = new CachedConstructorInfo<CachedConstructorInfoTests, string, int>();
            var obj = (CachedConstructorInfoTests)ctor.Invoke(new object[] { "foo", -1 });

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }

        [Test]
        public void CanInvokeWithStronglyTypedArgs()
        {
            var ctor = new CachedConstructorInfo<CachedConstructorInfoTests, string, int>();
            var obj = ctor.Invoke("foo", -1);

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }

        [Test]
        public void CanInvokeAsDynamic()
        {
            dynamic ctor = new CachedConstructorInfo<CachedConstructorInfoTests, string, int>();
            CachedConstructorInfoTests obj = ctor("foo", -1);

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }
    }
// ReSharper restore UnusedParameter.Local
}