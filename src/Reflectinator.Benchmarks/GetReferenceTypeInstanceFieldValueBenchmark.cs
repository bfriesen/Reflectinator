using System;

namespace Reflectinator.Benchmarks
{
    public class GetReferenceTypeInstanceFieldValueBenchmark : Benchmark
    {
        private static string _someValue = "some value";

        public GetReferenceTypeInstanceFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => _someValue = benchmark.BenchmarkInstanceFieldReferenceType; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _someValue = (string)_instanceFieldInfoReferenceType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _someValue = _instanceFieldStronglyTypedGetReferenceType.Value(benchmark);
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
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