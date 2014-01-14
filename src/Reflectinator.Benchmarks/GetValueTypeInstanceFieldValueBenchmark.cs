using System;

namespace Reflectinator.Benchmarks
{
    public class GetValueTypeInstanceFieldValueBenchmark : Benchmark
    {
        private static ulong _someValue = 123456789;

        public GetValueTypeInstanceFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => _someValue = benchmark.BenchmarkInstanceFieldValueType; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _someValue = (ulong)_instanceFieldInfoValueType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _someValue = _instanceFieldStronglyTypedGetValueType.Value(benchmark);
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _someValue = (ulong)_instanceFieldLooselyTypedGetValueType.Value(benchmark);
        }

        public override string Name
        {
            get { return "Get value-type instance field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}