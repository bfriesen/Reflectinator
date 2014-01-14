using System;

namespace Reflectinator.Benchmarks
{
    public class SetValueTypeStaticFieldValueBenchmark : Benchmark
    {
        private static ulong _someValue = 123456789;

        public SetValueTypeStaticFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => BenchmarkStaticFieldValueType = SomeValue; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _staticFieldInfoValueType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _staticFieldStronglyTypedSetValueType.Value(SomeValue);
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _staticFieldLooselyTypedSetValueType.Value(SomeValue);
        }

        public override string Name
        {
            get { return "Set value-type static field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}