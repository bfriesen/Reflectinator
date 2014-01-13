using System;

namespace Reflectinator.Benchmarks
{
    public class GetValueTypeInstancePropertyValueBenchmark : BenchmarkCommand
    {
        private static ulong _someValue = 123456789;

        public GetValueTypeInstancePropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<BenchmarkCommand> DirectAccess
        {
            get { return benchmark => _someValue = benchmark.BenchmarkInstanceFieldValueType; }
        }

        private static void Reflection(BenchmarkCommand benchmark)
        {
            _someValue = (ulong)_instancePropertyInfoValueType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(BenchmarkCommand benchmark)
        {
            _someValue = _instancePropertyStronglyTypedGetValueType.Value(benchmark);
        }

        private static void ReflectinatorLooselyTyped(BenchmarkCommand benchmark)
        {
            _someValue = (ulong)_instancePropertyLooselyTypedGetValueType.Value(benchmark);
        }

        public override string Name
        {
            get { return "Get value-type instance property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static ulong SomeValue { get { return _someValue; } }
    }
}