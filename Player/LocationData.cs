using System;
using Newtonsoft.Json;

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
public class LocationData
{
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