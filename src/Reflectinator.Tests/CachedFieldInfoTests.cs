using System.Reflection;
using NUnit.Framework;

namespace Reflectinator.Tests
{
// ReSharper disable InconsistentNaming
#pragma warning disable 169
    public class CachedFieldInfoTests
    {
        private const string _constantField = "foo";
        private string _instanceField = "bar";
        private static string _staticField = "baz";

        [Test]
        public void CanReadFromInstanceFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_instanceField", BindingFlags.NonPublic | BindingFlags.Instance));
            var iField = (ICachedFieldInfo) field;

            Assert.That(() => field.Get(this), Throws.Nothing);
            Assert.That(field.Get(this), Is.EqualTo("bar"));

            Assert.That(() => iField.Get(this), Throws.Nothing);
            Assert.That(iField.Get(this), Is.EqualTo("bar"));
        }

        [Test]
        public void CanWriteToInstanceFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_instanceField", BindingFlags.NonPublic | BindingFlags.Instance));
            var iField = (ICachedFieldInfo)field;

            Assert.That(() => field.Set(this, "wooo!"), Throws.Nothing);
            Assert.That(_instanceField, Is.EqualTo("wooo!"));

            Assert.That(() => iField.Set(this, "woah!"), Throws.Nothing);
            Assert.That(_instanceField, Is.EqualTo("woah!"));
        }

        [Test]
        public void CanReadFromStaticFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_staticField", BindingFlags.NonPublic | BindingFlags.Static));
            var iField = (ICachedFieldInfo) field;

            Assert.That(() => field.Get(this), Throws.Nothing);
            Assert.That(field.Get(this), Is.EqualTo("baz"));

            Assert.That(() => iField.Get(this), Throws.Nothing);
            Assert.That(iField.Get(this), Is.EqualTo("baz"));

            Assert.That(() => field.Get(), Throws.Nothing);
            Assert.That(field.Get(), Is.EqualTo("baz"));

            Assert.That(() => iField.Get(), Throws.Nothing);
            Assert.That(iField.Get(), Is.EqualTo("baz"));
        }

        [Test]
        public void CanWriteToStaticFields()
        {
            var field = new CachedFieldInfo<CachedFieldInfoTests, string>(GetType().GetField("_staticField", BindingFlags.NonPublic | BindingFlags.Static));
            var iField = (ICachedFieldInfo)field;

            Assert.That(() => field.Set(this, "wooo!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("wooo!"));

            Assert.That(() => iField.Set(this, "woah!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("woah!"));

            Assert.That(() => field.Set("wooo!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("wooo!"));

            Assert.That(() => iField.Set("woah!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("woah!"));
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