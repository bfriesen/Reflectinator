using NUnit.Framework;

namespace Reflectinator.Tests
{
// ReSharper disable UnusedParameter.Local
    public class ConstructorTests
    {
        private readonly string _s;
        private readonly int _i;

        public ConstructorTests()
        {
        }

        public ConstructorTests(string s)
        {
            _s = s;
        }

        public ConstructorTests(int i)
        {
            _i = i;
        }

        public ConstructorTests(string s, int i)
        {
            _s = s;
            _i = i;
        }

        [Test]
        public void CanSpecifyConstructorWithGenericArguments()
        {
            Assert.That(() => new Constructor<ConstructorTests>(), Throws.Nothing);
            Assert.That(() => new Constructor<ConstructorTests, string>(), Throws.Nothing);
            Assert.That(() => new Constructor<ConstructorTests, int>(), Throws.Nothing);
            Assert.That(() => new Constructor<ConstructorTests, string, int>(), Throws.Nothing);
        }

        [Test]
        public void InvalidConstructorTypesThrowsException()
        {
            Assert.That(() => new Constructor<ConstructorTests, decimal>(), Throws.Exception);
        }

        [Test]
        public void CanInvokeWithObjectArrayAsArgs()
        {
            var ctor = new Constructor<ConstructorTests, string, int>();
            var obj = (ConstructorTests)ctor.Invoke(new object[] { "foo", -1 });

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }

        [Test]
        public void CanInvokeWithStronglyTypedArgs()
        {
            var ctor = new Constructor<ConstructorTests, string, int>();
            var obj = ctor.Invoke("foo", -1);

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }

        [Test]
        public void CanInvokeAsDynamic()
        {
            dynamic ctor = new Constructor<ConstructorTests, string, int>();
            ConstructorTests obj = ctor("foo", -1);

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }
    }
// ReSharper restore UnusedParameter.Local
}