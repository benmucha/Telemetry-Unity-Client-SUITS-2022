using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
public class LocationData
{
    [Preserve]
    [JsonConstructor]
    public LocationData(bool fake_arg)
    {
        // fake_arg is to have a unique ctor that we exclusively
        // use in JSON de-serialization via JsonConstructor attribute.
        // Preserve attribute ensures Xamarin linker does not remove,
        // as there are no direct uses of this ctor in the code base
    }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("user")]
    public int UserId { get; set; }

    [JsonProperty("room")]
    public int RoomId { get; set; }

    [JsonProperty("latitude")]
    public float Latitude { get; set; }

    [JsonProperty("longitude")]
    public float Longitude { get; set; }

    [JsonProperty("altitude")]
    public float? Altitude { get; set; }

    [JsonProperty("accuracy")]
    public float? Accuracy { get; set; }

    [JsonProperty("altitudeAccuracy")]
    public float? AltitudeAccuracy { get; set; }

    [JsonProperty("heading")]
    public float? Heading { get; set; }

    [JsonProperty("speed")]
    public float? Speed { get; set; }

    [JsonProperty("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }
}