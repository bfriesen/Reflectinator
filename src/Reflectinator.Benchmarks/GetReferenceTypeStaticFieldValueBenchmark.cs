using System;

namespace Reflectinator.Benchmarks
{
    public class GetReferenceTypeStaticFieldValueBenchmark : BenchmarkCommand
    {
        private static string _someValue = "some value";

        public GetReferenceTypeStaticFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => _someValue = BenchmarkStaticFieldReferenceType; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _someValue = (string)_staticFieldInfoReferenceType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _someValue = _staticFieldStronglyTypedGetReferenceType.Value();
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _someValue = (string)_staticFieldLooselyTypedGetReferenceType.Value();
        }

        public override string Name
        {
            get { return "Get reference-type static field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}