using System;

namespace Reflectinator.Benchmarks
{
    public class SetReferenceTypeStaticFieldValueBenchmark : BenchmarkCommand
    {
        private static string _someValue = "some value";

        public SetReferenceTypeStaticFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => BenchmarkStaticFieldReferenceType = SomeValue; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _staticFieldInfoReferenceType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _staticFieldStronglyTypedSetReferenceType.Value(SomeValue);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _staticFieldLooselyTypedSetReferenceType.Value(SomeValue);
        }

        public override string Name
        {
            get { return "Set reference-type static field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}