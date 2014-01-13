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
            Assert.That(() => Constructor.Get<ConstructorTests>(), Throws.Nothing);
            Assert.That(() => Constructor.Get<ConstructorTests, string>(), Throws.Nothing);
            Assert.That(() => Constructor.Get<ConstructorTests, int>(), Throws.Nothing);
            Assert.That(() => Constructor.Get<ConstructorTests, string, int>(), Throws.Nothing);
        }

        [Test]
        public void InvalidConstructorTypesThrowsException()
        {
            Assert.That(() => Constructor.Get<ConstructorTests, decimal>(), Throws.Exception);
        }

        [Test]
        public void CanInvokeWithObjectArrayAsArgs()
        {
            var sut = Constructor.Get<ConstructorTests, string, int>();

            var obj = (ConstructorTests)sut.Invoke(new object[] { "foo", -1 });

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }

        [Test]
        public void CanInvokeWithStronglyTypedArgs()
        {
            var sut = Constructor.Get<ConstructorTests, string, int>();

            var obj = sut.Invoke("foo", -1);

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }

        [Test]
        public void CanInvokeAsDynamic()
        {
            dynamic sut = Constructor.Get<ConstructorTests, string, int>();

            ConstructorTests obj = sut("foo", -1);

            Assert.That(obj._s, Is.EqualTo("foo"));
            Assert.That(obj._i, Is.EqualTo(-1));
        }
    }
// ReSharper restore UnusedParameter.Local
}