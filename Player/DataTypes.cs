

namespace RISD.SuitsTelemetryClient.Data
{
    public abstract class ValueInRange
    {
        public abstract float Min { get; }
        public abstract float Max { get; }
        public float Value { get; }

        public float Percent => (Value - Min) / (Max - Min);

        protected ValueInRange(float value)
        {
            Value = value;
        }
    }

    public abstract class Pressure : ValueInRange
    {
        protected Pressure(float value) : base(value)
        {
        }
    }

    public class SuitPressure : Pressure
    {
        public override float Min => 2;
        public override float Max => 4;

        public SuitPressure(float value) : base(value)
        {
        }
    }

    public class H2OGasPressure : Pressure
    {
        public override float Min => 14;
        public override float Max => 16;

        public H2OGasPressure(float value) : base(value)
        {
        }
    }

    public class H2OLiquidPressure : Pressure
    {
        public override float Min => 14;
        public override float Max => 16;

        public H2OLiquidPressure(float value) : base(value)
        {
        }
    }

    public class OxygenPressure : Pressure
    {
        public override float Min => 750;
        public override float Max => 950;

        public OxygenPressure(float value) : base(value)
        {
        }
    }

    public class SopPressure : Pressure
    {
        public override float Min => 750;
        public override float Max => 950;

        public SopPressure(float value) : base(value)
        {
        }
    }

    public class SubPressure : Pressure
    {
        public override float Min => 2;
        public override float Max => 4;

        public SubPressure(float value) : base(value)
        {
        }
    }
}