using Newtonsoft.Json;
using System;
using RISD.SuitsTelemetryClient.Data;
using UnityEngine.Scripting;

public class UserInputModel
{
    [Preserve]
    [JsonConstructor]
    public UserInputModel()
    {
        // fake_arg is to have a unique ctor that we exclusively
        // use in JSON de-serialization via JsonConstructor attribute.
        // Preserve attribute ensures Xamarin linker does not remove,
        // as there are no direct uses of this ctor in the code base
    }

    [JsonProperty("username")]
    public string Username { get; set; }
    
    [JsonProperty("room")]
    public int Room { get; set; }
}

public class UserData
{
    [Preserve]
    [JsonConstructor]
    public UserData()
    {
        // fake_arg is to have a unique ctor that we exclusively
        // use in JSON de-serialization via JsonConstructor attribute.
        // Preserve attribute ensures Xamarin linker does not remove,
        // as there are no direct uses of this ctor in the code base
    }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("room")]
    public int Room { get; set; }

    [JsonProperty("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }
}

public class LsarMessageInputModel
{
    [Preserve]
    [JsonConstructor]
    public LsarMessageInputModel()
    {
        // fake_arg is to have a unique ctor that we exclusively
        // use in JSON de-serialization via JsonConstructor attribute.
        // Preserve attribute ensures Xamarin linker does not remove,
        // as there are no direct uses of this ctor in the code base
    }

    [JsonProperty("sender")]
    public int SenderId { get; set; }

    [JsonProperty("room")]
    public int RoomId { get; set; }

    /// <summary>
    /// 8 Character Message.
    /// </summary>
    [JsonProperty("vmc_txt")]
    public string Message { get; set; }
}

/// <summary>
/// <see cref="SimulationStateRoomData"/> from deserialized JSON.
/// </summary>
public class SimulationStateRoomData
{
    [Preserve]
    [JsonConstructor]
    public SimulationStateRoomData()
    {
        // fake_arg is to have a unique ctor that we exclusively
        // use in JSON de-serialization via JsonConstructor attribute.
        // Preserve attribute ensures Xamarin linker does not remove,
        // as there are no direct uses of this ctor in the code base
    }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("room")]
    public int Room { get; set; }

    [JsonProperty("isRunning")]
    public bool IsRunning { get; set; }

    [JsonProperty("isPaused")]
    public bool IsPaused { get; set; }

    /// <summary>
    /// Duration of the current EVA. EVA Time is displayed in the format “hh:mm:ss”
    /// </summary>
    [JsonProperty("time")]
    public float EVATime { get; set; }

    [JsonProperty("timer")]
    public string Timer { get; set; }

    [JsonProperty("started_at")]
    public string StartedAt { get; set; }

    /// <summary>
    /// Heart rate of the astronaut measured in beats per minute. Expected range is from 80 to 100 bpm.
    /// </summary>
    [JsonProperty("heart_bpm")]
    public int HeartRate { get; set; }


    /// <summary>
    /// External Environment pressure. Expected range is from 2 to 4 psia.
    /// </summary>
    [JsonProperty("p_sub")]
    private float _subPressure;
    public SubPressure SubPressure => new SubPressure(_subPressure);

    /// <summary>
    /// The pressure inside the spacesuit needs to stay within certain limits. If the suit pressure gets too high, or if the pressure exceeds nominal limits, the movement of the astronaut will be heavily reduced. Expected range is from 2 to 4 psid.
    /// </summary>
    [JsonProperty("p_suit")]
    public float _suitPressure { get; set; }
    public SuitPressure SuitPressure => new SuitPressure(_suitPressure);

    [JsonProperty("t_sub")]
    public float SubTime { get; set; }

    /// <summary>
    /// Speed of the cooling fan. Expected range is from 10,000 to 40,000 RPM.
    /// </summary>
    [JsonProperty("v_fan")]
    public int FanTachometer { get; set; }

    /// <summary>
    /// Pressure inside the Primary Oxygen Pack. Expected range is from 750 to 950 psia.
    /// </summary>
    [JsonProperty("p_o2")]
    public float _oxygenPressure { get; set; }
    public OxygenPressure OxygenPressure => new OxygenPressure(_oxygenPressure);

    /// <summary>
    /// Flowrate of the Primary Oxygen Pack. Expected range is from 0.5 to 1 psi/min.
    /// </summary>
    [JsonProperty("rate_o2")]
    public float OxygenRate { get; set; }

    [JsonProperty("batteryPercent")]
    public float BatteryPercent { get; set; }

    /// <summary>
    /// Total capacity of the spacesuit’s battery. Expected range is from 0 to 30 amp-hr.
    /// </summary>
    [JsonProperty("cap_battery")]
    public float BatteryCapacity { get; set; }

    [JsonProperty("battery_out")]
    public float BatteryOut { get; set; }

    /// <summary>
    /// Gas pressure from H2O system. Expected range is from 14 to 16 psia.
    /// </summary>
    [JsonProperty("p_h2o_g")]
    public float _h2OGasPressure { get; set; }
    public H2OGasPressure H2OGasPressure => new H2OGasPressure(_h2OGasPressure);

    /// <summary>
    /// Liquid pressure from H2O system. Expected range is from 14 to 16 psia.
    /// </summary>
    [JsonProperty("p_h2o_l")]
    public float _h2OLiquidPressure { get; set; }
    public H2OLiquidPressure H2OLiquidPressure => new H2OLiquidPressure(_h2OLiquidPressure);

    /// <summary>
    /// Pressure inside the Secondary Oxygen Pack. Expected range is from ? to ? psia.
    /// </summary>
    [JsonProperty("p_sop")]
    public float _sopPressure { get; set; }
    public SopPressure SopPressure => new SopPressure(_sopPressure);

    /// <summary>
    /// Flowrate of the Secondary Oxygen Pack. Expected range is from 0.5 to 1 psi/min.
    /// </summary>
    [JsonProperty("rate_sop")]
    public float SOPRate { get; set; }

    /// <summary>
    /// The remaining time until the battery of the spacesuit is completely discharged. Battery life is usually displayed in the format “hh:mm:ss” Expected range is from 0 to 10 hours.
    /// </summary>
    [JsonProperty("t_battery")]
    public string BatteryTimeLeft { get; set; }

    [JsonProperty("t_oxygenPrimary")]
    public int OxygenPrimaryTimeLeft { get; set; }

    [JsonProperty("t_oxygenSec")]
    public float OxygenSecondaryTimeLeft { get; set; }

    /// <summary>
    /// Percentage left in the primary oxygen supply.
    /// </summary>
    [JsonProperty("ox_primary")]
    public float PrimaryOxygen { get; set; }

    /// <summary>
    /// Percentage left in the secondary oxygen supply.
    /// </summary>
    [JsonProperty("ox_secondary")]
    public float SecondaryOxygen { get; set; }

    /// <summary>
    /// The remaining time until the available oxygen is depleted. Time life oxygen is usually displayed in the format “hh:mm:ss” Expected range is from 0 to 10 hours.
    /// </summary>
    [JsonProperty("t_oxygen")]
    public string OxygenTimeLeft { get; set; }

    [JsonProperty("cap_water")]
    public float WaterCapacity { get; set; }

    /// <summary>
    /// The remaining time until the water resources of the spacesuit are depleted. Time life water is usually displayed in the format “hh:mm:ss” Expected range is from 0 to 10 hours.
    /// </summary>
    [JsonProperty("t_water")]
    public string WaterTimeLeft { get; set; }

    [JsonProperty("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }
}