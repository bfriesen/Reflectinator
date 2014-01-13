using System;

namespace Reflectinator.Benchmarks
{
    public class SetValueTypeStaticPropertyValueBenchmark : BenchmarkCommand
    {
        private static ulong _someValue = 123456789;

        public SetValueTypeStaticPropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => BenchmarkStaticFieldValueType = SomeValue; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _staticPropertyInfoValueType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _staticPropertyStronglyTypedSetValueType.Value(SomeValue);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _staticPropertyLooselyTypedSetValueType.Value(SomeValue);
        }

        public override string Name
        {
            get { return "Set value-type static property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}