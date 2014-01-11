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
        public string ReadonlyProperty { get { return null; } }
// ReSharper disable once ValueParameterNotUsed
        public string WriteonlyProperty { set {} }

        [Test]
        public void CanReadFromProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("Property"));
            var iProperty = (ICachedPropertyInfo)property;

            Assert.That(() => property.Get(this), Throws.Nothing);
            Assert.That(property.Get(this), Is.EqualTo("bar"));

            Assert.That(() => iProperty.Get(this), Throws.Nothing);
            Assert.That(iProperty.Get(this), Is.EqualTo("bar"));
        }

        [Test]
        public void CanWriteToProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("Property"));
            var iProperty = (ICachedPropertyInfo)property;

            Assert.That(() => property.Set(this, "wooo!!"), Throws.Nothing);
            Assert.That(Property, Is.EqualTo("wooo!!"));

            Assert.That(() => iProperty.Set(this, "hooo!!"), Throws.Nothing);
            Assert.That(Property, Is.EqualTo("hooo!!"));
        }

        [Test]
        public void CannotWriteToReadonlyProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("ReadonlyProperty"));
            var iProperty = (ICachedPropertyInfo)property;

            Assert.That(() => property.Set(this, "wooo!!"), Throws.Exception);
            Assert.That(() => iProperty.Set(this, "wooo!!"), Throws.Exception);
        }

        [Test]
        public void CannotReadFromWriteonlyProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("WriteonlyProperty"));
            var iProperty = (ICachedPropertyInfo)property;

            Assert.That(() => property.Get(this), Throws.Exception);
            Assert.That(() => iProperty.Get(this), Throws.Exception);
        }
    }
}