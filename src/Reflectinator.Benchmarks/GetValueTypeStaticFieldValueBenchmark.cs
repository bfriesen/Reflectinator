using System;

namespace Reflectinator.Benchmarks
{
    public class GetValueTypeStaticFieldValueBenchmark : Benchmark
    {
        private static ulong _someValue = 123456789;

        public GetValueTypeStaticFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => _someValue = BenchmarkStaticFieldValueType; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _someValue = (ulong)_staticFieldInfoValueType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _someValue = _staticFieldStronglyTypedGetValueType.Value();
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _someValue = (ulong)_staticFieldLooselyTypedGetValueType.Value();
        }

        public override string Name
        {
            get { return "Get value-type static field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}