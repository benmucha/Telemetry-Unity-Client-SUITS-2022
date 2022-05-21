using System;

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
[Serializable]
public class LocationData
{ 
    public int id { get; set; }

    public int user { get; set; }

    public int room { get; set; }

    public float latitude { get; set; }

    public float longitude { get; set; }

    public float? altitude { get; set; }

    public float? accuracy { get; set; }

    public float? altitudeAccuracy { get; set; }

    public float? heading { get; set; }

    public float? speed { get; set; }

    public DateTimeOffset createdAt { get; set; }

    public DateTimeOffset updatedAt { get; set; }
}