using System;
using ManyConsole;

namespace Reflectinator.Benchmarks
{
    public class RunAllConsoleCommand : ConsoleCommand
    {
        private readonly BenchmarkCommand[] _benchmarkCommands;
        private double _factor = 1;

        public RunAllConsoleCommand()
        {
            this.IsCommand("run-all", "Run all benchmarks");
            this.HasOption<double>("factor|f=", "Multiplies the iteration count for each benchmark by this number. If benchmarks are running too fast or slow, experiment with this option. Minimum valid value is zero.", d => _factor = d);

            _benchmarkCommands = new BenchmarkCommand[]
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

            foreach (var benchmarkCommand in _benchmarkCommands)
            {
                benchmarkCommand.Execute(_factor);

                Console.WriteLine(benchmarkCommand.Name);
                Console.WriteLine("  Times:");
                Console.WriteLine("    Direct Access:                {0}", benchmarkCommand.DirectAccessTime);
                Console.WriteLine("    Reflection:                   {0}", benchmarkCommand.ReflectionTime);
                Console.WriteLine("    Strongly-typed Reflectinator: {0}", benchmarkCommand.ReflectinatorStronglyTypedTime);
                Console.WriteLine("    Loosely-typed Reflectinator:  {0}", benchmarkCommand.ReflectinatorLooselyTypedTime);
                Console.WriteLine("  Ratio of N / Direct Access:");
                Console.WriteLine("    Reflection:                   {0:0.00}", benchmarkCommand.ReflectionTimeNormalized);
                Console.WriteLine("    Strongly-typed Reflectinator: {0:0.00}", benchmarkCommand.ReflectinatorStronglyTypedTimeNormalized);
                Console.WriteLine("    Loosely-typed Reflectinator:  {0:0.00}", benchmarkCommand.ReflectinatorLooselyTypedTimeNormalized);
                Console.WriteLine();
            }

            return 0;
        }
    }
}