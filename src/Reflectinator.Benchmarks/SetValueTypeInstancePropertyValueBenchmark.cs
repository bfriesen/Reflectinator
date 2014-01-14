using System;

namespace Reflectinator.Benchmarks
{
    public class SetValueTypeInstancePropertyValueBenchmark : Benchmark
    {
        private static ulong _someValue = 123456789;

        public SetValueTypeInstancePropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => benchmark.BenchmarkInstanceFieldValueType = SomeValue; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _instancePropertyInfoValueType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _instancePropertyStronglyTypedSetValueType.Value(benchmark, SomeValue);
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _instancePropertyLooselyTypedSetValueType.Value(benchmark, SomeValue);
        }

        public override string Name
        {
            get { return "Set value-type instance property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}