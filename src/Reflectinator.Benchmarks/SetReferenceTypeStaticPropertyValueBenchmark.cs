using System;

namespace Reflectinator.Benchmarks
{
    public class SetReferenceTypeStaticPropertyValueBenchmark : BenchmarkCommand
    {
        private static string _someValue = "some value";

        public SetReferenceTypeStaticPropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => BenchmarkStaticFieldReferenceType = SomeValue; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _staticPropertyInfoReferenceType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _staticPropertyStronglyTypedSetReferenceType.Value(SomeValue);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _staticPropertyLooselyTypedSetReferenceType.Value(SomeValue);
        }

        public override string Name
        {
            get { return "Set reference-type static property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}