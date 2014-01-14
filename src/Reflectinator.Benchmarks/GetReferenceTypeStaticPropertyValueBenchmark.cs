using System;

namespace Reflectinator.Benchmarks
{
    public class GetReferenceTypeStaticPropertyValueBenchmark : Benchmark
    {
        private static string _someValue = "some value";

        public GetReferenceTypeStaticPropertyValueBenchmark()
            : base(DirectAccess, Reflection, ReflectinatorStronglyTyped, ReflectinatorLooselyTyped)
        {
        }

        private static Action<Benchmark> DirectAccess
        {
            get { return benchmark => _someValue = BenchmarkStaticFieldReferenceType; }
        }

        private static void Reflection(Benchmark benchmark)
        {
            _someValue = (string)_staticPropertyInfoReferenceType.Value.GetValue(benchmark);
        }

        private static void ReflectinatorStronglyTyped(Benchmark benchmark)
        {
            _someValue = _staticPropertyStronglyTypedGetReferenceType.Value();
        }

        private static void ReflectinatorLooselyTyped(Benchmark benchmark)
        {
            _someValue = (string)_staticPropertyLooselyTypedGetReferenceType.Value();
        }

        public override string Name
        {
            get { return "Get reference-type static property"; }
        }

        protected override ulong BaseIterations
        {
            get { return 10 * 1000 * 1000; }
        }

        public static string SomeValue { get { return _someValue; } }
    }
}