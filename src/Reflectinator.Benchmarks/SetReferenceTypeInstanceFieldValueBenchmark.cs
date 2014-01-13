using System;

namespace Reflectinator.Benchmarks
{
    public class SetReferenceTypeInstanceFieldValueBenchmark : BenchmarkCommand
    {
        private static string _someValue = "some value";

        public SetReferenceTypeInstanceFieldValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => benchmark.BenchmarkInstanceFieldReferenceType = SomeValue; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _instanceFieldInfoReferenceType.Value.SetValue(benchmark, SomeValue);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _instanceFieldStronglyTypedSetReferenceType.Value(benchmark, SomeValue);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _instanceFieldLooselyTypedSetReferenceType.Value(benchmark, SomeValue);
        }

        public override string Name
        {
            get { return "Set reference-type instance field"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}