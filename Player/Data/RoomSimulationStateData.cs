using System;
using RISD.SuitsTelemetryClient.Data;

[Serializable]
public class LsarMessageInputModel
{
    public int sender;

    public int room;

    /// <summary>
    /// 8 Character Message.
    /// </summary>
    public string vmc_txt;
}

[Serializable]
public class SimulationStateList
{
    public SimulationStateRoomData[] list;

    public static string FormattedJsonStringWrapper => "{{\"list\":{0}}}";
}

/// <summary>
/// <see cref="SimulationStateRoomData"/> from deserialized JSON.
/// </summary>
[Serializable]
public class SimulationStateRoomData
{
    public int id;

    public int room;

    public bool isRunning;

    public bool isPaused;

    /// <summary>
    /// Duration of the current EVA. EVA Time is displayed in the format “hh:mm:ss”
    /// </summary>
    public float time;

    public string timer;

    public string started_at;

    /// <summary>
    /// Heart rate of the astronaut measured in beats per minute. Expected range is from 80 to 100 bpm.
    /// </summary>
    public int heart_bpm;


    /// <summary>
    /// External Environment pressure. Expected range is from 2 to 4 psia.
    /// </summary>
    private float p_sub;
    public SubPressure SubPressure => new SubPressure(p_sub);

    /// <summary>
    /// The pressure inside the spacesuit needs to stay within certain limits. If the suit pressure gets too high, or if the pressure exceeds nominal limits, the movement of the astronaut will be heavily reduced. Expected range is from 2 to 4 psid.
    /// </summary>
    public float p_suit;
    public SuitPressure SuitPressure => new SuitPressure(p_suit);

    public float t_sub;

    /// <summary>
    /// Speed of the cooling fan. Expected range is from 10,000 to 40,000 RPM.
    /// </summary>
    public int v_fan;

    /// <summary>
    /// Pressure inside the Primary Oxygen Pack. Expected range is from 750 to 950 psia.
    /// </summary>
    public float p_o2;
    public OxygenPressure OxygenPressure => new OxygenPressure(p_o2);

    /// <summary>
    /// Flowrate of the Primary Oxygen Pack. Expected range is from 0.5 to 1 psi/min.
    /// </summary>
    public float rate_o2;

    public float batteryPercent;

    /// <summary>
    /// Total capacity of the spacesuit’s battery. Expected range is from 0 to 30 amp-hr.
    /// </summary>
    public float cap_battery;

    public float battery_out;

    /// <summary>
    /// Gas pressure from H2O system. Expected range is from 14 to 16 psia.
    /// </summary>
    public float p_h2o_g;
    public H2OGasPressure H2OGasPressure => new H2OGasPressure(p_h2o_g);

    /// <summary>
    /// Liquid pressure from H2O system. Expected range is from 14 to 16 psia.
    /// </summary>
    public float p_h2o_l;
    public H2OLiquidPressure H2OLiquidPressure => new H2OLiquidPressure(p_h2o_l);

    /// <summary>
    /// Pressure inside the Secondary Oxygen Pack. Expected range is from ? to ? psia.
    /// </summary>
    public float p_sop;
    public SopPressure SopPressure => new SopPressure(p_sop);

    /// <summary>
    /// Flowrate of the Secondary Oxygen Pack. Expected range is from 0.5 to 1 psi/min.
    /// </summary>
    public float rate_sop;

    /// <summary>
    /// The remaining time until the battery of the spacesuit is completely discharged. Battery life is usually displayed in the format “hh:mm:ss” Expected range is from 0 to 10 hours.
    /// </summary>
    public string t_battery;

    public int t_oxygenPrimary;

    public float t_oxygenSec;

    /// <summary>
    /// Percentage left in the primary oxygen supply.
    /// </summary>
    public float ox_primary;

    /// <summary>
    /// Percentage left in the secondary oxygen supply.
    /// </summary>
    public float ox_secondary;

    /// <summary>
    /// The remaining time until the available oxygen is depleted. Time life oxygen is usually displayed in the format “hh:mm:ss” Expected range is from 0 to 10 hours.
    /// </summary>
    public string t_oxygen;

    public float cap_water;

    /// <summary>
    /// The remaining time until the water resources of the spacesuit are depleted. Time life water is usually displayed in the format “hh:mm:ss” Expected range is from 0 to 10 hours.
    /// </summary>
    public string t_water;

    public DateTimeOffset createdAt;

    public DateTimeOffset updatedAt;
}