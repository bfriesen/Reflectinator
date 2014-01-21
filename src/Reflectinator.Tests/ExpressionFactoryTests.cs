using System;
using NUnit.Framework;

namespace Reflectinator.Tests
{
    public class ExpressionFactoryTests
    {
        [Test]
        public void ConstFieldsThrowMemberAccessExceptionWhenWrittenTo()
        {
            var fieldInfo = typeof(Foo).GetField("SomeConstField");

            var looseSetExpression = ExpressionFactory.CreateSetValueFuncExpression(fieldInfo);
            var setExpression = ExpressionFactory.CreateSetValueFuncExpression<Foo, string>(fieldInfo);

            var looseSet = looseSetExpression.Compile();
            var set = setExpression.Compile();

            var foo = new Foo();

            Assert.That(() => looseSet(foo, "foo"), Throws.TypeOf<MemberAccessException>());
            Assert.That(() => set(foo, "foo"), Throws.TypeOf<MemberAccessException>());
        }

        [Test]
        public void ReadOnlyFieldsThrowMemberAccessExceptionWhenWrittenTo()
        {
            var fieldInfo = typeof(Foo).GetField("SomeReadonlyField");

            var looseSetExpression = ExpressionFactory.CreateSetValueFuncExpression(fieldInfo);
            var setExpression = ExpressionFactory.CreateSetValueFuncExpression<Foo, string>(fieldInfo);

            var looseSet = looseSetExpression.Compile();
            var set = setExpression.Compile();

            var foo = new Foo();

            Assert.That(() => looseSet(foo, "foo"), Throws.TypeOf<MemberAccessException>());
            Assert.That(() => set(foo, "foo"), Throws.TypeOf<MemberAccessException>());
        }

        [Test]
        public void WriteOnlyPropertiesThrowMemberAccessExceptionWhenReadFrom()
        {
            var propertyInfo = typeof(Foo).GetProperty("SomeWriteonlyProperty");

            var looseGetExpression = ExpressionFactory.CreateGetValueFuncExpression(propertyInfo);
            var getExpression = ExpressionFactory.CreateGetValueFuncExpression<Foo, string>(propertyInfo);

            var looseGet = looseGetExpression.Compile();
            var get = getExpression.Compile();

            var foo = new Foo();

            Assert.That(() => looseGet(foo), Throws.TypeOf<MemberAccessException>());
            Assert.That(() => get(foo), Throws.TypeOf<MemberAccessException>());
        }

        [Test]
        public void ReadOnlyPropertiesThrowMemberAccessExceptionWhenWrittenTo()
        {
            var propertyInfo = typeof(Foo).GetProperty("SomeReadonlyProperty");

            var looseSetExpression = ExpressionFactory.CreateSetValueFuncExpression(propertyInfo);
            var setExpression = ExpressionFactory.CreateSetValueFuncExpression<Foo, string>(propertyInfo);

            var looseSet = looseSetExpression.Compile();
            var set = setExpression.Compile();

            var foo = new Foo();

            Assert.That(() => looseSet(foo, "foo"), Throws.TypeOf<MemberAccessException>());
            Assert.That(() => set(foo, "foo"), Throws.TypeOf<MemberAccessException>());
        }

        public class Foo
        {
            private string _someInstanceField;
            private static string _someStaticField;

            public Foo()
            {
            }

            public Foo(int i)
            {
            }

            public Foo(string s, int i)
            {

            }

            public string AddTheseInstanceFunc(int lhs, int rhs)
            {
                return string.Format("{0} + {1} = {2}", lhs, rhs, lhs + rhs);
            }

            public void AddTheseInstanceAction(int lhs, int rhs)
            {
                string.Format("{0} + {1} = {2}", lhs, rhs, lhs + rhs);
            }

            public static string AddTheseStaticFunc(int lhs, int rhs)
            {
                return string.Format("{0} + {1} = {2}", lhs, rhs, lhs + rhs);
            }

            public static void AddTheseStaticAction(int lhs, int rhs)
            {
                string.Format("{0} + {1} = {2}", lhs, rhs, lhs + rhs);
            }

            public string SomeInstanceProperty
            {
                get { return _someInstanceField; }
                set { _someInstanceField = value; }
            }

            public static string SomeStaticProperty
            {
                get { return _someStaticField; }
                set { _someStaticField = value; }
            }

            public string SomeReadonlyProperty { get { return "Hello, world!"; } }
            public string SomeWriteonlyProperty { set {} }

            public const string SomeConstField = "foo!";
            public readonly string SomeReadonlyField = "foo!";
        }
    }
}