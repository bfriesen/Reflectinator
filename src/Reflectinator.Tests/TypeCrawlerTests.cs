using System;
using System.Linq;
using NUnit.Framework;

namespace Reflectinator.Tests
{
    public class TypeCrawlerTests
    {
        [Test]
        public void HasCorrectConstructors()
        {
            var sut = TypeCrawler.Create<Foo>();

            Assert.That(sut.Constructors.Length, Is.EqualTo(3));
            Assert.That(sut.Constructors.Count(x => x.Parameters.Length == 0), Is.EqualTo(1));
            Assert.That(sut.Constructors.Count(x => x.Parameters.Length == 1 && x.Parameters[0].Type == typeof(string)), Is.EqualTo(1));
            Assert.That(sut.Constructors.Count(x => x.Parameters.Length == 2 && x.Parameters[0].Type == typeof(string) && x.Parameters[1].Type == typeof(int)), Is.EqualTo(1));
            Assert.That(sut.Constructors.Count(x => x.ConstructorInfo.GetParameters().Any(p => p.ParameterType.IsPointer)), Is.EqualTo(0));
        }

        [Test]
        public void HasCorrectFields()
        {
            var sut = TypeCrawler.Create<Foo>();

            Assert.That(sut.Fields.Length, Is.EqualTo(10));
            Assert.That(sut.Fields.Count(x => x.IsConstant), Is.EqualTo(2));
            Assert.That(sut.Fields.Count(x => x.IsPublic), Is.EqualTo(5));
            Assert.That(sut.Fields.Count(x => x.IsReadOnly), Is.EqualTo(6));
            Assert.That(sut.Fields.Count(x => x.IsStatic), Is.EqualTo(6));
            Assert.That(sut.Fields.Count(x => x.FieldInfo.FieldType.IsPointer), Is.EqualTo(0));
        }

        [Test]
        public void HasCorrectProperties()
        {
            var sut = TypeCrawler.Create<Foo>();

            Assert.That(sut.Properties.Length, Is.EqualTo(8));
            Assert.That(sut.Properties.Count(x => x.IsPublic), Is.EqualTo(4));
            Assert.That(sut.Properties.Count(x => x.IsStatic), Is.EqualTo(4));
            Assert.That(sut.Properties.Count(x => x.CanRead), Is.EqualTo(8));
            Assert.That(sut.Properties.Count(x => x.CanWrite), Is.EqualTo(4));
            Assert.That(sut.Properties.Count(x => x.PropertyInfo.PropertyType.IsPointer), Is.EqualTo(0));
        }

// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable InconsistentNaming
#pragma warning disable 169
// ReSharper disable ConvertToConstant.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
        private unsafe class Foo
        {
            private const string _constant = "foo";
            public const string PublicConstant = "bar";

            public static readonly string PublicStaticReadonlyField = "foo";
            public static string PublicStaticField;

            private static readonly string _staticReadonlyField = "bar";
            private static string _staticField;

            public readonly string _publicReadonlyField = "baz";
            public string _publicField;

            private readonly string _readonlyField = "baz";
            private string _field;

            private char* _charPtrField = (char*)IntPtr.Zero;

            public Foo()
            {
            }

            public Foo(string s)
            {
            }

            public Foo(string s, int i)
            {
            }

            public static string StaticReadonlyProperty { get { return _staticReadonlyField; } }
            public static string StaticProperty { get { return _staticField; } set { _staticField = value; } }

            private static string PrivateStaticReadonlyProperty { get { return _staticReadonlyField; } }
            private static string PrivateStaticProperty { get { return _staticField; } set { _staticField = value; } }

            public string ReadonlyProperty { get { return _readonlyField; } }
            public string Property { get { return _field; } set { _field = value; } }

            private string PrivateReadonlyProperty { get { return _readonlyField; } }
            private string PrivateProperty { get { return _field; } set { _field = value; } }

            public char* CharPtrProperty { get { return _charPtrField; } set { _charPtrField = value; } }
        }
    }
// ReSharper restore UnusedParameter.Local
// ReSharper restore UnusedMember.Local
// ReSharper restore ConvertToConstant.Local
#pragma warning restore 169
// ReSharper restore InconsistentNaming
// ReSharper restore ClassNeverInstantiated.Local
}