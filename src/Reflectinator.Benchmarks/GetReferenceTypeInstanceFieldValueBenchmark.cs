using System;

namespace Reflectinator.Benchmarks
{
    public class GetReferenceTypeInstanceFieldValueBenchmark : BenchmarkCommand
    {
        private static string _someValue = "some value";

        public GetReferenceTypeInstanceFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => _someValue = benchmark.BenchmarkInstanceFieldReferenceType; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _someValue = (string)_instanceFieldInfoReferenceType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _someValue = _instanceFieldStronglyTypedGetReferenceType.Value(benchmark);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _someValue = (string)_instanceFieldLooselyTypedGetReferenceType.Value(benchmark);
        }

        public override string Name
        {
            get { return "Get reference-type instance field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}