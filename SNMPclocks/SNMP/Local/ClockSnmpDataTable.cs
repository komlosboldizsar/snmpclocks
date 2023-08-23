using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

namespace SNMPclocks.SNMP.Local
{
    public class ClockSnmpDataTable : ObjectDataTable<Clock>
    {

        protected override IVariableFactory[] VariableFactories => new IVariableFactory[]
        {
            VARFACT_Id,
            VARFACT_Mode,
            VARFACT_State,
            VARFACT_ValueSeconds,
            VARFACT_ValueStringHhMm,
            VARFACT_ValueStringHhMmSs,
            VARFACT_DoStart,
            VARFACT_DoStop,
            VARFACT_DoReset
        };

        protected override IVariableFactory IndexerVariableFactory => VARFACT_Id;

        public const int INDEX_Id = 1;
        public const int INDEX_Mode = 2;
        public const int INDEX_State = 3;
        public const int INDEX_ValueSeconds = 4;
        public const int INDEX_ValueStringHhMm = 5;
        public const int INDEX_ValueStringHhMmSs = 6;
        public const int INDEX_DoStart = 21;
        public const int INDEX_DoStop = 22;
        public const int INDEX_DoReset = 23;

        public static readonly IVariableFactory VARFACT_Id = new VariableFactory<DataProviders.Id>(INDEX_Id);
        public static readonly IVariableFactory VARFACT_Mode = new VariableFactory<DataProviders.Mode>(INDEX_Mode);
        public static readonly IVariableFactory VARFACT_State = new VariableFactory<DataProviders.State>(INDEX_State);
        public static readonly IVariableFactory VARFACT_ValueSeconds = new VariableFactory<DataProviders.ValueSeconds>(INDEX_ValueSeconds);
        public static readonly IVariableFactory VARFACT_ValueStringHhMm = new VariableFactory<DataProviders.ValueStringHhMm>(INDEX_ValueStringHhMm);
        public static readonly IVariableFactory VARFACT_ValueStringHhMmSs = new VariableFactory<DataProviders.ValueStringHhMmSs>(INDEX_ValueStringHhMmSs);
        public static readonly IVariableFactory VARFACT_DoStart = new VariableFactory<DataProviders.DoStart>(INDEX_DoStart);
        public static readonly IVariableFactory VARFACT_DoStop = new VariableFactory<DataProviders.DoStop>(INDEX_DoStop);
        public static readonly IVariableFactory VARFACT_DoReset = new VariableFactory<DataProviders.DoReset>(INDEX_DoReset);

        protected override ITrapGeneratorFactory[] TrapGeneratorFactories => new ITrapGeneratorFactory[]
        {
            TRAPGENFACT_1
        };

        public static readonly ITrapGeneratorFactory TRAPGENFACT_1 = new TrapGeneratorFactory<TrapGenerators.TimeChanged>();

        protected override string TableOid => $"{SnmpAgent.OID_BASE}.1";
        protected override int ItemIndex => Model.ID;

        private class DataProviders
        {

            // .1
            public class Id : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32(Model.ID);
            }

            // .2
            public class Mode : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32((int)Model.Mode);
            }

            // .3
            public class State : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32((int)Model.State);
            }

            // .4
            public class ValueSeconds : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32(Model.ValueSeconds);
            }

            // .5
            public class ValueStringHhMm : VariableDataProvider
            {
                public override ISnmpData Get() => new OctetString(Model.ValueStringHhMmSs[..5]);
            }

            // .6
            public class ValueStringHhMmSs : VariableDataProvider
            {
                public override ISnmpData Get() => new OctetString(Model.ValueStringHhMmSs);
            }

            // .21
            public class DoStart : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32(0);
                public override void Set(ISnmpData data)
                    => _do(data, "start", () => Model.Start());
            }

            // .22
            public class DoStop : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32(0);
                public override void Set(ISnmpData data)
                    => _do(data, "stop", () => Model.Stop());
            }

            // .23
            public class DoReset : VariableDataProvider
            {
                public override ISnmpData Get() => new Integer32(0);
                public override void Set(ISnmpData data)
                    => _do(data, "reset", () => Model.Reset());
            }

            private static void _do(ISnmpData data, string verb, Action successAction)
            {
                if (data is not Integer32 intData)
                    throw new SnmpErrorCodeException(ErrorCode.WrongType, $"Value must be a TruthValue ({TruthValue.VALUE_TRUE} or {TruthValue.VALUE_FALSE}) and set to 1 ({TruthValue.VALUE_TRUE}) to {verb} the clock.");
                int value = intData.ToInt32();
                if (value != TruthValue.VALUE_TRUE && value != TruthValue.VALUE_FALSE)
                    throw new SnmpErrorCodeException(ErrorCode.WrongValue);
                if (value != TruthValue.VALUE_TRUE)
                    return;
                successAction.Invoke();
            }

        }

        private class TrapGenerators
        {

            public class TimeChanged : TrapGenerator
            {

                public override string Code => "clockTimeChanged";
                public override string EnterpriseBase => $"{SnmpAgent.OID_BASE}.2";
                public override int SpecificCode => 1001;

                public override IEnumerable<IVariableFactory> PayloadVariableFactories => new IVariableFactory[]
                {
                    VARFACT_ValueSeconds,
                    VARFACT_ValueStringHhMmSs
                };

                public override void Subscribe()
                    => Table.Model.TimeChanged += timeChangedHandler;

                public override void Unsubscribe()
                    => Table.Model.TimeChanged -= timeChangedHandler;

                private void timeChangedHandler(Clock obj)
                    => SendTrap();

            }

        }

    }
}
