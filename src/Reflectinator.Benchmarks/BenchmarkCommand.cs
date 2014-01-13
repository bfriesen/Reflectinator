using System;
using System.Diagnostics;
using System.Reflection;

namespace Reflectinator.Benchmarks
{
    public abstract class BenchmarkCommand
    {
        protected static readonly Lazy<PropertyInfo> _instancePropertyInfoValueType;
        protected static readonly Lazy<Property<BenchmarkCommand, ulong>> _instancePropertyStronglyTypedValueType;
        protected static readonly Lazy<IProperty> _instancePropertyLooselyTypedValueType;
        protected static readonly Lazy<Func<BenchmarkCommand, ulong>> _instancePropertyStronglyTypedGetValueType;
        protected static readonly Lazy<Action<BenchmarkCommand, ulong>> _instancePropertyStronglyTypedSetValueType;
        protected static readonly Lazy<Func<object, object>> _instancePropertyLooselyTypedGetValueType;
        protected static readonly Lazy<Action<object, object>> _instancePropertyLooselyTypedSetValueType;

        protected static readonly Lazy<PropertyInfo> _staticPropertyInfoValueType;
        protected static readonly Lazy<Property<BenchmarkCommand, ulong>> _staticPropertyStronglyTypedValueType;
        protected static readonly Lazy<IProperty> _staticPropertyLooselyTypedValueType;
        protected static readonly Lazy<Func<ulong>> _staticPropertyStronglyTypedGetValueType;
        protected static readonly Lazy<Action<ulong>> _staticPropertyStronglyTypedSetValueType;
        protected static readonly Lazy<Func<object>> _staticPropertyLooselyTypedGetValueType;
        protected static readonly Lazy<Action<object>> _staticPropertyLooselyTypedSetValueType;

        protected static readonly Lazy<FieldInfo> _instanceFieldInfoValueType;
        protected static readonly Lazy<Field<BenchmarkCommand, ulong>> _instanceFieldStronglyTypedValueType;
        protected static readonly Lazy<IField> _instanceFieldLooselyTypedValueType;
        protected static readonly Lazy<Func<BenchmarkCommand, ulong>> _instanceFieldStronglyTypedGetValueType;
        protected static readonly Lazy<Action<BenchmarkCommand, ulong>> _instanceFieldStronglyTypedSetValueType;
        protected static readonly Lazy<Func<object, object>> _instanceFieldLooselyTypedGetValueType;
        protected static readonly Lazy<Action<object, object>> _instanceFieldLooselyTypedSetValueType;

        protected static readonly Lazy<FieldInfo> _staticFieldInfoValueType;
        protected static readonly Lazy<Field<BenchmarkCommand, ulong>> _staticFieldStronglyTypedValueType;
        protected static readonly Lazy<IField> _staticFieldLooselyTypedValueType;
        protected static readonly Lazy<Func<ulong>> _staticFieldStronglyTypedGetValueType;
        protected static readonly Lazy<Action<ulong>> _staticFieldStronglyTypedSetValueType;
        protected static readonly Lazy<Func<object>> _staticFieldLooselyTypedGetValueType;
        protected static readonly Lazy<Action<object>> _staticFieldLooselyTypedSetValueType;

        protected static readonly Lazy<PropertyInfo> _instancePropertyInfoReferenceType;
        protected static readonly Lazy<Property<BenchmarkCommand, string>> _instancePropertyStronglyTypedReferenceType;
        protected static readonly Lazy<IProperty> _instancePropertyLooselyTypedReferenceType;
        protected static readonly Lazy<Func<BenchmarkCommand, string>> _instancePropertyStronglyTypedGetReferenceType;
        protected static readonly Lazy<Action<BenchmarkCommand, string>> _instancePropertyStronglyTypedSetReferenceType;
        protected static readonly Lazy<Func<object, object>> _instancePropertyLooselyTypedGetReferenceType;
        protected static readonly Lazy<Action<object, object>> _instancePropertyLooselyTypedSetReferenceType;

        protected static readonly Lazy<PropertyInfo> _staticPropertyInfoReferenceType;
        protected static readonly Lazy<Property<BenchmarkCommand, string>> _staticPropertyStronglyTypedReferenceType;
        protected static readonly Lazy<IProperty> _staticPropertyLooselyTypedReferenceType;
        protected static readonly Lazy<Func<string>> _staticPropertyStronglyTypedGetReferenceType;
        protected static readonly Lazy<Action<string>> _staticPropertyStronglyTypedSetReferenceType;
        protected static readonly Lazy<Func<object>> _staticPropertyLooselyTypedGetReferenceType;
        protected static readonly Lazy<Action<object>> _staticPropertyLooselyTypedSetReferenceType;

        protected static readonly Lazy<FieldInfo> _instanceFieldInfoReferenceType;
        protected static readonly Lazy<Field<BenchmarkCommand, string>> _instanceFieldStronglyTypedReferenceType;
        protected static readonly Lazy<IField> _instanceFieldLooselyTypedReferenceType;
        protected static readonly Lazy<Func<BenchmarkCommand, string>> _instanceFieldStronglyTypedGetReferenceType;
        protected static readonly Lazy<Action<BenchmarkCommand, string>> _instanceFieldStronglyTypedSetReferenceType;
        protected static readonly Lazy<Func<object, object>> _instanceFieldLooselyTypedGetReferenceType;
        protected static readonly Lazy<Action<object, object>> _instanceFieldLooselyTypedSetReferenceType;

        protected static readonly Lazy<FieldInfo> _staticFieldInfoReferenceType;
        protected static readonly Lazy<Field<BenchmarkCommand, string>> _staticFieldStronglyTypedReferenceType;
        protected static readonly Lazy<IField> _staticFieldLooselyTypedReferenceType;
        protected static readonly Lazy<Func<string>> _staticFieldStronglyTypedGetReferenceType;
        protected static readonly Lazy<Action<string>> _staticFieldStronglyTypedSetReferenceType;
        protected static readonly Lazy<Func<object>> _staticFieldLooselyTypedGetReferenceType;
        protected static readonly Lazy<Action<object>> _staticFieldLooselyTypedSetReferenceType;

        private readonly Action<BenchmarkCommand> _directAccess;
        private readonly Action<BenchmarkCommand> _reflection;
        private readonly Action<BenchmarkCommand> _reflectinatorStronglyTyped;
        private readonly Action<BenchmarkCommand> _reflectinatorLooselyTyped;

        static BenchmarkCommand()
        {
            _instancePropertyInfoValueType = new Lazy<PropertyInfo>(() => typeof(BenchmarkCommand).GetProperty("BenchmarkInstancePropertyValueType"));
            _instancePropertyStronglyTypedValueType = new Lazy<Property<BenchmarkCommand, ulong>>(() => Property.Get<BenchmarkCommand, ulong>(_instancePropertyInfoValueType.Value));
            _instancePropertyLooselyTypedValueType = new Lazy<IProperty>(() => Property.Get(_instancePropertyInfoValueType.Value));
            _instancePropertyStronglyTypedGetValueType = new Lazy<Func<BenchmarkCommand, ulong>>(() => _instancePropertyStronglyTypedValueType.Value.GetFunc);
            _instancePropertyStronglyTypedSetValueType = new Lazy<Action<BenchmarkCommand, ulong>>(() => _instancePropertyStronglyTypedValueType.Value.SetAction);
            _instancePropertyLooselyTypedGetValueType = new Lazy<Func<object, object>>(() => _instancePropertyLooselyTypedValueType.Value.GetFunc);
            _instancePropertyLooselyTypedSetValueType = new Lazy<Action<object, object>>(() => _instancePropertyLooselyTypedValueType.Value.SetAction);

            _staticPropertyInfoValueType = new Lazy<PropertyInfo>(() => typeof(BenchmarkCommand).GetProperty("BenchmarkStaticPropertyValueType", BindingFlags.Public | BindingFlags.Static));
            _staticPropertyStronglyTypedValueType = new Lazy<Property<BenchmarkCommand, ulong>>(() => Property.Get<BenchmarkCommand, ulong>(_staticPropertyInfoValueType.Value));
            _staticPropertyLooselyTypedValueType = new Lazy<IProperty>(() => Property.Get(_staticPropertyInfoValueType.Value));
            _staticPropertyStronglyTypedGetValueType = new Lazy<Func<ulong>>(() => _staticPropertyStronglyTypedValueType.Value.GetStaticFunc);
            _staticPropertyStronglyTypedSetValueType = new Lazy<Action<ulong>>(() => _staticPropertyStronglyTypedValueType.Value.SetStaticAction);
            _staticPropertyLooselyTypedGetValueType = new Lazy<Func<object>>(() => _staticPropertyLooselyTypedValueType.Value.GetStaticFunc);
            _staticPropertyLooselyTypedSetValueType = new Lazy<Action<object>>(() => _staticPropertyLooselyTypedValueType.Value.SetStaticAction);

            _instanceFieldInfoValueType = new Lazy<FieldInfo>(() => typeof(BenchmarkCommand).GetField("BenchmarkInstanceFieldValueType"));
            _instanceFieldStronglyTypedValueType = new Lazy<Field<BenchmarkCommand, ulong>>(() => Field.Get<BenchmarkCommand, ulong>(_instanceFieldInfoValueType.Value));
            _instanceFieldLooselyTypedValueType = new Lazy<IField>(() => Field.Get(_instanceFieldInfoValueType.Value));
            _instanceFieldStronglyTypedGetValueType = new Lazy<Func<BenchmarkCommand, ulong>>(() => _instanceFieldStronglyTypedValueType.Value.GetFunc);
            _instanceFieldStronglyTypedSetValueType = new Lazy<Action<BenchmarkCommand, ulong>>(() => _instanceFieldStronglyTypedValueType.Value.SetAction);
            _instanceFieldLooselyTypedGetValueType = new Lazy<Func<object, object>>(() => _instanceFieldLooselyTypedValueType.Value.GetFunc);
            _instanceFieldLooselyTypedSetValueType = new Lazy<Action<object, object>>(() => _instanceFieldLooselyTypedValueType.Value.SetAction);

            _staticFieldInfoValueType = new Lazy<FieldInfo>(() => typeof(BenchmarkCommand).GetField("BenchmarkStaticFieldValueType", BindingFlags.Public | BindingFlags.Static));
            _staticFieldStronglyTypedValueType = new Lazy<Field<BenchmarkCommand, ulong>>(() => Field.Get<BenchmarkCommand, ulong>(_staticFieldInfoValueType.Value));
            _staticFieldLooselyTypedValueType = new Lazy<IField>(() => Field.Get(_staticFieldInfoValueType.Value));
            _staticFieldStronglyTypedGetValueType = new Lazy<Func<ulong>>(() => _staticFieldStronglyTypedValueType.Value.GetStaticFunc);
            _staticFieldStronglyTypedSetValueType = new Lazy<Action<ulong>>(() => _staticFieldStronglyTypedValueType.Value.SetStaticAction);
            _staticFieldLooselyTypedGetValueType = new Lazy<Func<object>>(() => _staticFieldLooselyTypedValueType.Value.GetStaticFunc);
            _staticFieldLooselyTypedSetValueType = new Lazy<Action<object>>(() => _staticFieldLooselyTypedValueType.Value.SetStaticAction);

            _instancePropertyInfoReferenceType = new Lazy<PropertyInfo>(() => typeof(BenchmarkCommand).GetProperty("BenchmarkInstancePropertyReferenceType"));
            _instancePropertyStronglyTypedReferenceType = new Lazy<Property<BenchmarkCommand, string>>(() => Property.Get<BenchmarkCommand, string>(_instancePropertyInfoReferenceType.Value));
            _instancePropertyLooselyTypedReferenceType = new Lazy<IProperty>(() => Property.Get(_instancePropertyInfoReferenceType.Value));
            _instancePropertyStronglyTypedGetReferenceType = new Lazy<Func<BenchmarkCommand, string>>(() => _instancePropertyStronglyTypedReferenceType.Value.GetFunc);
            _instancePropertyStronglyTypedSetReferenceType = new Lazy<Action<BenchmarkCommand, string>>(() => _instancePropertyStronglyTypedReferenceType.Value.SetAction);
            _instancePropertyLooselyTypedGetReferenceType = new Lazy<Func<object, object>>(() => _instancePropertyLooselyTypedReferenceType.Value.GetFunc);
            _instancePropertyLooselyTypedSetReferenceType = new Lazy<Action<object, object>>(() => _instancePropertyLooselyTypedReferenceType.Value.SetAction);

            _staticPropertyInfoReferenceType = new Lazy<PropertyInfo>(() => typeof(BenchmarkCommand).GetProperty("BenchmarkStaticPropertyReferenceType", BindingFlags.Public | BindingFlags.Static));
            _staticPropertyStronglyTypedReferenceType = new Lazy<Property<BenchmarkCommand, string>>(() => Property.Get<BenchmarkCommand, string>(_staticPropertyInfoReferenceType.Value));
            _staticPropertyLooselyTypedReferenceType = new Lazy<IProperty>(() => Property.Get(_staticPropertyInfoReferenceType.Value));
            _staticPropertyStronglyTypedGetReferenceType = new Lazy<Func<string>>(() => _staticPropertyStronglyTypedReferenceType.Value.GetStaticFunc);
            _staticPropertyStronglyTypedSetReferenceType = new Lazy<Action<string>>(() => _staticPropertyStronglyTypedReferenceType.Value.SetStaticAction);
            _staticPropertyLooselyTypedGetReferenceType = new Lazy<Func<object>>(() => _staticPropertyLooselyTypedReferenceType.Value.GetStaticFunc);
            _staticPropertyLooselyTypedSetReferenceType = new Lazy<Action<object>>(() => _staticPropertyLooselyTypedReferenceType.Value.SetStaticAction);

            _instanceFieldInfoReferenceType = new Lazy<FieldInfo>(() => typeof(BenchmarkCommand).GetField("BenchmarkInstanceFieldReferenceType"));
            _instanceFieldStronglyTypedReferenceType = new Lazy<Field<BenchmarkCommand, string>>(() => Field.Get<BenchmarkCommand, string>(_instanceFieldInfoReferenceType.Value));
            _instanceFieldLooselyTypedReferenceType = new Lazy<IField>(() => Field.Get(_instanceFieldInfoReferenceType.Value));
            _instanceFieldStronglyTypedGetReferenceType = new Lazy<Func<BenchmarkCommand, string>>(() => _instanceFieldStronglyTypedReferenceType.Value.GetFunc);
            _instanceFieldStronglyTypedSetReferenceType = new Lazy<Action<BenchmarkCommand, string>>(() => _instanceFieldStronglyTypedReferenceType.Value.SetAction);
            _instanceFieldLooselyTypedGetReferenceType = new Lazy<Func<object, object>>(() => _instanceFieldLooselyTypedReferenceType.Value.GetFunc);
            _instanceFieldLooselyTypedSetReferenceType = new Lazy<Action<object, object>>(() => _instanceFieldLooselyTypedReferenceType.Value.SetAction);

            _staticFieldInfoReferenceType = new Lazy<FieldInfo>(() => typeof(BenchmarkCommand).GetField("BenchmarkStaticFieldReferenceType", BindingFlags.Public | BindingFlags.Static));
            _staticFieldStronglyTypedReferenceType = new Lazy<Field<BenchmarkCommand, string>>(() => Field.Get<BenchmarkCommand, string>(_staticFieldInfoReferenceType.Value));
            _staticFieldLooselyTypedReferenceType = new Lazy<IField>(() => Field.Get(_staticFieldInfoReferenceType.Value));
            _staticFieldStronglyTypedGetReferenceType = new Lazy<Func<string>>(() => _staticFieldStronglyTypedReferenceType.Value.GetStaticFunc);
            _staticFieldStronglyTypedSetReferenceType = new Lazy<Action<string>>(() => _staticFieldStronglyTypedReferenceType.Value.SetStaticAction);
            _staticFieldLooselyTypedGetReferenceType = new Lazy<Func<object>>(() => _staticFieldLooselyTypedReferenceType.Value.GetStaticFunc);
            _staticFieldLooselyTypedSetReferenceType = new Lazy<Action<object>>(() => _staticFieldLooselyTypedReferenceType.Value.SetStaticAction);
        }

        protected BenchmarkCommand(
            Action<BenchmarkCommand> directAccess,
            Action<BenchmarkCommand> reflection,
            Action<BenchmarkCommand> reflectinatorStronglyTyped,
            Action<BenchmarkCommand> reflectinatorLooselyTyped)
        {
            _directAccess = directAccess;
            _reflection = reflection;
            _reflectinatorStronglyTyped = reflectinatorStronglyTyped;
            _reflectinatorLooselyTyped = reflectinatorLooselyTyped;

            // Get the functions warmed up.
            directAccess(this);
            reflection(this);
            reflectinatorStronglyTyped(this);
            reflectinatorLooselyTyped(this);
        }

        public void Execute()
        {
            Execute(1);
        }

        public void Execute(double factor)
        {
            var iterations = Convert.ToUInt64(BaseIterations * factor);

            DirectAccessTime = PerformBenchmarks(_directAccess, iterations);
            ReflectionTime = PerformBenchmarks(_reflection, iterations);
            ReflectinatorStronglyTypedTime = PerformBenchmarks(_reflectinatorStronglyTyped, iterations);
            ReflectinatorLooselyTypedTime = PerformBenchmarks(_reflectinatorLooselyTyped, iterations);
        }

        public TimeSpan DirectAccessTime { get; private set; }
        public TimeSpan ReflectionTime { get; private set; }
        public TimeSpan ReflectinatorStronglyTypedTime { get; private set; }
        public TimeSpan ReflectinatorLooselyTypedTime { get; private set; }

        public double ReflectionTimeNormalized { get { return ReflectionTime.TotalMilliseconds / DirectAccessTime.TotalMilliseconds; } }
        public double ReflectinatorStronglyTypedTimeNormalized { get { return ReflectinatorStronglyTypedTime.TotalMilliseconds / DirectAccessTime.TotalMilliseconds; } }
        public double ReflectinatorLooselyTypedTimeNormalized { get { return ReflectinatorLooselyTypedTime.TotalMilliseconds / DirectAccessTime.TotalMilliseconds; } }

        public abstract string Name { get; }
        protected abstract ulong BaseIterations { get; }

        public static ulong BenchmarkStaticFieldValueType;
        public ulong BenchmarkInstanceFieldValueType;

        public static ulong BenchmarkStaticPropertyValueType { get { return BenchmarkStaticFieldValueType; } set { BenchmarkStaticFieldValueType = value; } }
        public ulong BenchmarkInstancePropertyValueType { get { return BenchmarkInstanceFieldValueType; } set { BenchmarkInstanceFieldValueType = value; } }

        public static string BenchmarkStaticFieldReferenceType;
        public string BenchmarkInstanceFieldReferenceType;

        public static string BenchmarkStaticPropertyReferenceType { get { return BenchmarkStaticFieldReferenceType; } set { BenchmarkStaticFieldReferenceType = value; } }
        public string BenchmarkInstancePropertyReferenceType { get { return BenchmarkInstanceFieldReferenceType; } set { BenchmarkInstanceFieldReferenceType = value; } }

        private TimeSpan PerformBenchmarks(Action<BenchmarkCommand> action, ulong iterations)
        {
            var stopwatch = Stopwatch.StartNew();

            for (ulong i = 0; i < iterations; i++)
            {
                action(this);
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}