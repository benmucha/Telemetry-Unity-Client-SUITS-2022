using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
public class LsarMessageData
{
    [Preserve]
    [JsonConstructor]
    public LsarMessageData(bool fake_arg)
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

    [JsonProperty("sender")]
    public int SenderId { get; set; }

    [JsonProperty("isPaused")]
    public bool IsPaused { get; set; }

    [JsonProperty("time")]
    public string Time { get; set; }

    [JsonProperty("priority_tag")]
    public string PriorityTag { get; set; }

    [JsonProperty("encoded_lat")]
    public string EncodedLatitude { get; set; }

    [JsonProperty("encoded_lon")]
    public string EncodedLongitude { get; set; }

    [JsonProperty("pnt_source")]
    public string PntSource { get; set; }

    [JsonProperty("condition_state")]
    public string ConditionState { get; set; }

    [JsonProperty("vmc_txt")]
    public string VmcText { get; set; }

    [JsonProperty("tac_sn")]
    public string TacSn { get; set; }

    [JsonProperty("cntry_code")]
    public string CountryCode { get; set; }

    [JsonProperty("homing_dvc_stat")]
    public string HomingDvcStat { get; set; }

    [JsonProperty("ret_lnk_stat")]
    public string RetLnkStat { get; set; }

    [JsonProperty("test_pronto")]
    public string TestPronto { get; set; }

    [JsonProperty("vessel_id")]
    public string VesselId { get; set; }

    [JsonProperty("beacon_type")]
    public string BeaconType { get; set; }

    [JsonProperty("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }
}
