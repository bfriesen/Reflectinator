using System;

namespace Reflectinator.Benchmarks
{
    public class SetValueTypeInstancePropertyValueBenchmark : BenchmarkCommand
    {
        private static ulong _someValue = 123456789;

        public SetValueTypeInstancePropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => benchmark.BenchmarkInstanceFieldValueType = SomeValue; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _instancePropertyInfoValueType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _instancePropertyStronglyTypedSetValueType.Value(benchmark, SomeValue);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
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