using NUnit.Framework;

namespace Reflectinator.Tests
{
    public class CachedPropertyInfoTests
    {
        public CachedPropertyInfoTests()
        {
            Property = "bar";
        }

        public string Property { get; set; }
        public string ReadonlyProperty { get { return "foo"; } }

        [Test]
        public void CanReadFromProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("Property"));

            Assert.That(() => property.Get(this), Throws.Nothing);
            Assert.That(property.Get(this), Is.EqualTo("bar"));
        }

        [Test]
        public void CanWriteToProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("Property"));

            Assert.That(() => property.Set(this, "wooo!!"), Throws.Nothing);
            Assert.That(property.Get(this), Is.EqualTo("wooo!!"));
        }

        [Test]
        public void CannotWriteToReadonlyProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("ReadonlyProperty"));

            Assert.That(() => property.Set(this, "wooo!!"), Throws.Exception);
        }
    }
}