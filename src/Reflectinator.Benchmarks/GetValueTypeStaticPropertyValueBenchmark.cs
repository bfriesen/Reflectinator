using System;

namespace Reflectinator.Benchmarks
{
    public class GetValueTypeStaticPropertyValueBenchmark : Benchmark
    {
        private static ulong _someValue = 123456789;

        public GetValueTypeStaticPropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => _someValue = BenchmarkStaticFieldValueType; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _someValue = (ulong)_staticPropertyInfoValueType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _someValue = _staticPropertyStronglyTypedGetValueType.Value();
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _someValue = (ulong)_staticPropertyLooselyTypedGetValueType.Value();
        }

        public override string Name
        {
            get { return "Get value-type static property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}