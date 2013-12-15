using System.Reflection;
using NUnit.Framework;

namespace Reflectinator.Tests
{
// ReSharper disable InconsistentNaming
#pragma warning disable 169
    public class CachedFieldInfoTests
    {
        private const string _constantField = "foo";
        private string _field = "bar";

        [Test]
        public void CanReadFromFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_field", BindingFlags.NonPublic | BindingFlags.Instance));

            Assert.That(() => field.Get(this), Throws.Nothing);
            Assert.That(field.Get(this), Is.EqualTo("bar"));
        }

        [Test]
        public void CanWriteToFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_field", BindingFlags.NonPublic | BindingFlags.Instance));

            Assert.That(() => field.Set(this, "wooo!"), Throws.Nothing);
            Assert.That(field.Get(this), Is.EqualTo("wooo!"));
        }

        [Test]
        public void CannotWriteToConstantFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_constantField", BindingFlags.NonPublic | BindingFlags.Static));

            Assert.That(() => field.Set(this, "wooo!"), Throws.Exception);
        }
    }
#pragma warning restore 169
// ReSharper restore InconsistentNaming
}