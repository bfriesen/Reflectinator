using NUnit.Framework;

namespace Reflectinator.Tests
{
    public class MethodTests
    {
        [Test]
        public void SmokeTest()
        {
            var methodInfo = this.GetType().GetMethod("InstanceMethod");

            var method = Method.Get(methodInfo);

            var returnValue = method.Invoke(this, 2, 2.5);

            Assert.That(returnValue, Is.EqualTo("5"));
        }

        public string InstanceMethod(int i, double d)
        {
            return (i * d).ToString();
        }
    }
}