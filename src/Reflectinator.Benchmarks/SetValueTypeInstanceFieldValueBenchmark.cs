using System;

namespace Reflectinator.Benchmarks
{
    public class SetValueTypeInstanceFieldValueBenchmark : Benchmark
    {
        private static ulong _someValue = 123456789;

        public SetValueTypeInstanceFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => benchmark.BenchmarkInstanceFieldValueType = SomeValue; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _instanceFieldInfoValueType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _instanceFieldStronglyTypedSetValueType.Value(benchmark, SomeValue);
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _instanceFieldLooselyTypedSetValueType.Value(benchmark, SomeValue);
        }

        public override string Name
        {
            get { return "Set value-type instance field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}