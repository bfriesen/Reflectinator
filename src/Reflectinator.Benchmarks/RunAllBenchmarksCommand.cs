using System;
using ManyConsole;

namespace Reflectinator.Benchmarks
{
    public class RunBenchmarkCommand : ConsoleCommand
    {
        private double _factor = 1;
        private bool? _isStatic;
        private BenchmarkType _benchmarkType;
        private bool _isValueType;
        private bool? _isGet;

        public RunBenchmarkCommand()
        {
            this.IsCommand("run", "Run benchmark");
            this.HasOption<double>("factor|f=", "Multiplies the iteration count for each benchmark by this number. If benchmarks are running too fast or slow, experiment with this option. Minimum valid value is zero.", d => _factor = d);
            this.HasOption("static|instance", "Whether the benchmark should be against an instance or static member", option => _isStatic = option.ToLower() == "static");
            this.HasRequiredOption<BenchmarkType>("field|property", "The benchmark type", benchmarkType => _benchmarkType = benchmarkType);
            this.HasRequiredOption("value|reference", "Whether the benchmark should use a value type or a reference type", option => _isValueType = option.ToLower() == "value");
            this.HasOption("get|set", "Whether the benchmark should get or set its value", option => _isGet = option.ToLower() == "get");
        }

        public override int Run(string[] remainingArguments)
        {
            Benchmark benchmark;

            if (_isGet == true && _isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new GetValueTypeStaticFieldValueBenchmark();
            }
            else if (_isGet == true && !_isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new GetReferenceTypeStaticFieldValueBenchmark();
            }
            else if (_isGet == true && _isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new GetValueTypeStaticPropertyValueBenchmark();
            }
            else if (_isGet == true && !_isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new GetReferenceTypeStaticPropertyValueBenchmark();
            }
            else if (_isGet == true && _isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new GetValueTypeInstanceFieldValueBenchmark();
            }
            else if (_isGet == true && !_isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new GetReferenceTypeInstanceFieldValueBenchmark();
            }
            else if (_isGet == true && _isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new GetValueTypeInstancePropertyValueBenchmark();
            }
            else if (_isGet == true && !_isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new GetReferenceTypeInstancePropertyValueBenchmark();
            }
            else if (_isGet == false && _isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new SetValueTypeStaticFieldValueBenchmark();
            }
            else if (_isGet == false && !_isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new SetReferenceTypeStaticFieldValueBenchmark();
            }
            else if (_isGet == false && _isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new SetValueTypeStaticPropertyValueBenchmark();
            }
            else if (_isGet == false && !_isValueType && _isStatic == true && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new SetReferenceTypeStaticPropertyValueBenchmark();
            }
            else if (_isGet == false && _isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new SetValueTypeInstanceFieldValueBenchmark();
            }
            else if (_isGet == false && !_isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Field)
            {
                benchmark = new SetReferenceTypeInstanceFieldValueBenchmark();
            }
            else if (_isGet == false && _isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new SetValueTypeInstancePropertyValueBenchmark();
            }
            else if (_isGet == false && !_isValueType && _isStatic == false && _benchmarkType == BenchmarkType.Property)
            {
                benchmark = new SetReferenceTypeInstancePropertyValueBenchmark();
            }
            //else if (_isValueType && _benchmarkType == BenchmarkType.Constructor)
            //{
            //    benchmark = new InvokeValueTypeConstructorBenchmark();
            //}
            //else if (!_isValueType && _benchmarkType == BenchmarkType.Constructor)
            //{
            //    benchmark = new InvokeReferenceTypeConstructorBenchmark();
            //}
            else
            {
                return 1;
            }

            Console.WriteLine(benchmark.Name);

            benchmark.Execute(_factor);

            Console.WriteLine("  Times:");
            Console.WriteLine("    Direct Access:                {0}", benchmark.DirectAccessTime);
            Console.WriteLine("    Reflection:                   {0}", benchmark.ReflectionTime);
            Console.WriteLine("    Strongly-typed Reflectinator: {0}", benchmark.ReflectinatorStronglyTypedTime);
            Console.WriteLine("    Loosely-typed Reflectinator:  {0}", benchmark.ReflectinatorLooselyTypedTime);
            Console.WriteLine("  Ratio of N / Direct Access:");
            Console.WriteLine("    Reflection:                   {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
            Console.WriteLine("    Strongly-typed Reflectinator: {0:0.00}", benchmark.ReflectinatorStronglyTypedTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
            Console.WriteLine("    Loosely-typed Reflectinator:  {0:0.00}", benchmark.ReflectinatorLooselyTypedTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
            Console.WriteLine("  Ratio of Reflection / N:");
            Console.WriteLine("    Direct Access                 {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
            Console.WriteLine("    Strongly-typed Reflectinator: {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.ReflectinatorStronglyTypedTime.TotalMilliseconds);
            Console.WriteLine("    Loosely-typed Reflectinator:  {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.ReflectinatorLooselyTypedTime.TotalMilliseconds);

            return 0;
        }

        private enum BenchmarkType
        {
            Field,
            Property,
            //Constructor,
        }
    }

    public class RunAllBenchmarksCommand : ConsoleCommand
    {
        private readonly Benchmark[] _benchmarks;
        private double _factor = 1;

        public RunAllBenchmarksCommand()
        {
            this.IsCommand("run-all", "Run all benchmarks");
            this.HasOption<double>("factor|f=", "Multiplies the iteration count for each benchmark by this number. If benchmarks are running too fast or slow, experiment with this option. Minimum valid value is zero.", d => _factor = d);

            _benchmarks = new Benchmark[]
            {
                new GetValueTypeInstancePropertyValueBenchmark(),
                new SetValueTypeInstancePropertyValueBenchmark(),
                new GetReferenceTypeInstancePropertyValueBenchmark(),
                new SetReferenceTypeInstancePropertyValueBenchmark(),

                new GetValueTypeStaticPropertyValueBenchmark(),
                new SetValueTypeStaticPropertyValueBenchmark(),
                new GetReferenceTypeStaticPropertyValueBenchmark(),
                new SetReferenceTypeStaticPropertyValueBenchmark(),

                new GetValueTypeInstanceFieldValueBenchmark(),
                new SetValueTypeInstanceFieldValueBenchmark(),
                new GetReferenceTypeInstanceFieldValueBenchmark(),
                new SetReferenceTypeInstanceFieldValueBenchmark(),

                new GetValueTypeStaticFieldValueBenchmark(),
                new SetValueTypeStaticFieldValueBenchmark(),
                new GetReferenceTypeStaticFieldValueBenchmark(),
                new SetReferenceTypeStaticFieldValueBenchmark(),
            };
        }

        public override int Run(string[] remainingArguments)
        {
            // Get and display machine stats.

            foreach (var benchmark in _benchmarks)
            {
                Console.WriteLine(benchmark.Name);

                benchmark.Execute(_factor);

                Console.WriteLine("  Times:");
                Console.WriteLine("    Direct Access:                {0}", benchmark.DirectAccessTime);
                Console.WriteLine("    Reflection:                   {0}", benchmark.ReflectionTime);
                Console.WriteLine("    Strongly-typed Reflectinator: {0}", benchmark.ReflectinatorStronglyTypedTime);
                Console.WriteLine("    Loosely-typed Reflectinator:  {0}", benchmark.ReflectinatorLooselyTypedTime);
                Console.WriteLine("  Ratio of N / Direct Access:");
                Console.WriteLine("    Reflection:                   {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
                Console.WriteLine("    Strongly-typed Reflectinator: {0:0.00}", benchmark.ReflectinatorStronglyTypedTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
                Console.WriteLine("    Loosely-typed Reflectinator:  {0:0.00}", benchmark.ReflectinatorLooselyTypedTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
                Console.WriteLine("  Ratio of Reflection / N:");
                Console.WriteLine("    Direct Access                 {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.DirectAccessTime.TotalMilliseconds);
                Console.WriteLine("    Strongly-typed Reflectinator: {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.ReflectinatorStronglyTypedTime.TotalMilliseconds);
                Console.WriteLine("    Loosely-typed Reflectinator:  {0:0.00}", benchmark.ReflectionTime.TotalMilliseconds / benchmark.ReflectinatorLooselyTypedTime.TotalMilliseconds);
                Console.WriteLine();
            }
            /*
                    public double ReflectionTimeNormalized { get { return ReflectionTime.TotalMilliseconds / DirectAccessTime.TotalMilliseconds; } }
                    public double ReflectinatorStronglyTypedTimeNormalized { get { return ReflectinatorStronglyTypedTime.TotalMilliseconds / DirectAccessTime.TotalMilliseconds; } }
                    public double ReflectinatorLooselyTypedTimeNormalized { get { return ReflectinatorLooselyTypedTime.TotalMilliseconds / DirectAccessTime.TotalMilliseconds; } }
            */
            return 0;
        }
    }
}