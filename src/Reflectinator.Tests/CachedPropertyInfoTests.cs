using System.Reflection;
using NUnit.Framework;

namespace Reflectinator.Tests
{
    public class CachedPropertyInfoTests
    {
        static CachedPropertyInfoTests()
        {
            StaticProperty = "foo";
        }

        public CachedPropertyInfoTests()
        {
            Property = "bar";
        }

        public string Property { get; set; }
        public string ReadonlyProperty { get { return null; } }
        public string WriteonlyProperty { set {} }
        public static string StaticProperty { get; set; }

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

        [Test]
        public void MismatchedTDeclaringTypeAndPropertyInfoDeclaringTypeThrowsException()
        {
            Assert.That(() => new CachedPropertyInfo<CachedPropertyInfoTests, string>(typeof(Foo).GetProperty("Bar")), Throws.Exception);
        }

        [Test]
        public void MismatchedTPropertyTypeAndPropertyInfoPropertyTypeThrowsException()
        {
            Assert.That(() => new CachedPropertyInfo<Foo, int>(typeof(Foo).GetProperty("Bar")), Throws.Exception);
        }

        [Test]
        public void CanGetAndSetStaticProperties()
        {
            var property = new CachedPropertyInfo<CachedPropertyInfoTests, string>(GetType().GetProperty("StaticProperty", BindingFlags.Public | BindingFlags.Static));
            var iProperty = (ICachedPropertyInfo) property;

            Assert.That(() => property.GetAsStatic(), Throws.Nothing);
            Assert.That(property.GetAsStatic(), Is.EqualTo("foo"));

            Assert.That(() => property.SetAsStatic("rawr!"), Throws.Nothing);
            Assert.That(StaticProperty, Is.EqualTo("rawr!"));

            Assert.That(() => iProperty.GetAsStatic(), Throws.Nothing);
            Assert.That(iProperty.GetAsStatic(), Is.EqualTo("rawr!"));

            Assert.That(() => iProperty.SetAsStatic("foo"), Throws.Nothing);
            Assert.That(StaticProperty, Is.EqualTo("foo"));
        }

        private class Foo
        {
            public string Bar { get; set; }
        }
    }
}