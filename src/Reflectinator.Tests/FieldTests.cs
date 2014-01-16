using System.Reflection;
using NUnit.Framework;

namespace Reflectinator.Tests
{
// ReSharper disable InconsistentNaming
#pragma warning disable 169
    public class FieldTests
    {
        private const string _constantField = "foo";
        private string _instanceField = "bar";
        private static string _staticField = "baz";

        [Test]
        public void CanReadFromInstanceFields()
        {
            _instanceField = "bar";

            var sut = Field.Get<FieldTests, string>(GetType().GetField("_instanceField", BindingFlags.NonPublic | BindingFlags.Instance));
            var iSut = (IField) sut;

            Assert.That(() => sut.Get(this), Throws.Nothing);
            Assert.That(sut.Get(this), Is.EqualTo("bar"));

            Assert.That(() => iSut.Get(this), Throws.Nothing);
            Assert.That(iSut.Get(this), Is.EqualTo("bar"));
        }

        [Test]
        public void CanWriteToInstanceFields()
        {
            var sut = Field.Get<FieldTests, string>(GetType().GetField("_instanceField", BindingFlags.NonPublic | BindingFlags.Instance));
            var iSut = (IField)sut;

            Assert.That(() => sut.Set(this, "wooo!"), Throws.Nothing);
            Assert.That(_instanceField, Is.EqualTo("wooo!"));

            Assert.That(() => iSut.Set(this, "woah!"), Throws.Nothing);
            Assert.That(_instanceField, Is.EqualTo("woah!"));
        }

        [Test]
        public void CanReadFromStaticFields()
        {
            _staticField = "baz";

            var sut = Field.GetStatic<FieldTests, string>(GetType().GetField("_staticField", BindingFlags.NonPublic | BindingFlags.Static));
            var iSut = (IStaticField) sut;

            Assert.That(() => sut.Get(this), Throws.Nothing);
            Assert.That(sut.Get(this), Is.EqualTo("baz"));

            Assert.That(() => iSut.Get(this), Throws.Nothing);
            Assert.That(iSut.Get(this), Is.EqualTo("baz"));

            Assert.That(() => sut.Get(), Throws.Nothing);
            Assert.That(sut.Get(), Is.EqualTo("baz"));

            Assert.That(() => iSut.Get(), Throws.Nothing);
            Assert.That(iSut.Get(), Is.EqualTo("baz"));
        }

        [Test]
        public void CanWriteToStaticFields()
        {
            var sut = Field.GetStatic<FieldTests, string>(GetType().GetField("_staticField", BindingFlags.NonPublic | BindingFlags.Static));
            var iSut = (IStaticField)sut;

            Assert.That(() => sut.Set(this, "wooo!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("wooo!"));

            Assert.That(() => iSut.Set(this, "woah!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("woah!"));

            Assert.That(() => sut.Set("wooo!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("wooo!"));

            Assert.That(() => iSut.Set("woah!"), Throws.Nothing);
            Assert.That(_staticField, Is.EqualTo("woah!"));
        }

        [Test]
        public void CannotWriteToConstantFields()
        {
            var sut = Field.Get<FieldTests, string>(GetType().GetField("_constantField", BindingFlags.NonPublic | BindingFlags.Static));

            Assert.That(() => sut.Set(this, "wooo!"), Throws.Exception);
        }
    }
#pragma warning restore 169
// ReSharper restore InconsistentNaming
}