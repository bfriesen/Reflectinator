using System.Reflection;
using NUnit.Framework;

namespace Reflectinator.Tests
{
    public class PropertyTests
    {
        static PropertyTests()
        {
            StaticProperty = "foo";
        }

        public PropertyTests()
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
            var sut = new Property<PropertyTests, string>(GetType().GetProperty("Property"));
            var iSut = (IProperty)sut;

            Assert.That(() => sut.Get(this), Throws.Nothing);
            Assert.That(sut.Get(this), Is.EqualTo("bar"));

            Assert.That(() => iSut.Get(this), Throws.Nothing);
            Assert.That(iSut.Get(this), Is.EqualTo("bar"));
        }

        [Test]
        public void CanWriteToProperties()
        {
            var sut = new Property<PropertyTests, string>(GetType().GetProperty("Property"));
            var iSut = (IProperty)sut;

            Assert.That(() => sut.Set(this, "wooo!!"), Throws.Nothing);
            Assert.That(Property, Is.EqualTo("wooo!!"));

            Assert.That(() => iSut.Set(this, "hooo!!"), Throws.Nothing);
            Assert.That(Property, Is.EqualTo("hooo!!"));
        }

        [Test]
        public void CannotWriteToReadonlyProperties()
        {
            var sut = new Property<PropertyTests, string>(GetType().GetProperty("ReadonlyProperty"));
            var iSut = (IProperty)sut;

            Assert.That(() => sut.Set(this, "wooo!!"), Throws.Exception);
            Assert.That(() => iSut.Set(this, "wooo!!"), Throws.Exception);
        }

        [Test]
        public void CannotReadFromWriteonlyProperties()
        {
            var sut = new Property<PropertyTests, string>(GetType().GetProperty("WriteonlyProperty"));
            var iSut = (IProperty)sut;

            Assert.That(() => sut.Get(this), Throws.Exception);
            Assert.That(() => iSut.Get(this), Throws.Exception);
        }

        [Test]
        public void MismatchedTDeclaringTypeAndPropertyInfoDeclaringTypeThrowsException()
        {
            Assert.That(() => new Property<PropertyTests, string>(typeof(Foo).GetProperty("Bar")), Throws.Exception);
        }

        [Test]
        public void MismatchedTPropertyTypeAndPropertyInfoPropertyTypeThrowsException()
        {
            Assert.That(() => new Property<Foo, int>(typeof(Foo).GetProperty("Bar")), Throws.Exception);
        }

        [Test]
        public void CanGetAndSetStaticProperties()
        {
            var sut = new Property<PropertyTests, string>(GetType().GetProperty("StaticProperty", BindingFlags.Public | BindingFlags.Static));
            var iSut = (IProperty) sut;

            Assert.That(() => sut.Get(), Throws.Nothing);
            Assert.That(sut.Get(), Is.EqualTo("foo"));

            Assert.That(() => sut.Set("rawr!"), Throws.Nothing);
            Assert.That(StaticProperty, Is.EqualTo("rawr!"));

            Assert.That(() => iSut.Get(), Throws.Nothing);
            Assert.That(iSut.Get(), Is.EqualTo("rawr!"));

            Assert.That(() => iSut.Set("foo"), Throws.Nothing);
            Assert.That(StaticProperty, Is.EqualTo("foo"));
        }

        private class Foo
        {
            public string Bar { get; set; }
        }
    }
}