using System;

namespace Reflectinator.Benchmarks
{
    public class SetValueTypeInstanceFieldValueBenchmark : BenchmarkCommand
    {
        private static ulong _someValue = 123456789;

        public SetValueTypeInstanceFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => benchmark.BenchmarkInstanceFieldValueType = SomeValue; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _instanceFieldInfoValueType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _instanceFieldStronglyTypedSetValueType.Value(benchmark, SomeValue);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
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