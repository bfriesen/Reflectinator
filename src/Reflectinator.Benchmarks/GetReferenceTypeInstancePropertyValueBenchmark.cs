using System;

namespace Reflectinator.Benchmarks
{
    public class GetReferenceTypeInstancePropertyValueBenchmark : BenchmarkCommand
    {
        private static string _someValue = "some value";

        public GetReferenceTypeInstancePropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => _someValue = benchmark.BenchmarkInstanceFieldReferenceType; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _someValue = (string)_instancePropertyInfoReferenceType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _someValue = _instancePropertyStronglyTypedGetReferenceType.Value(benchmark);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _someValue = (string)_instancePropertyLooselyTypedGetReferenceType.Value(benchmark);
        }

        public override string Name
        {
            get { return "Get reference-type instance property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}