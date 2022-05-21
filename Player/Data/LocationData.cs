using System;

[Serializable]
public class LocationsList
{
    public LocationData[] list;

    public static string FormattedJsonStringWrapper => "{{\"list\":{0}}}";
}

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
[Serializable]
public class LocationData
{
    public int id;

    public int user;

    public int room;

    public float latitude;

    public float longitude;

    public float? altitude;

    public float? accuracy;

    public float? altitudeAccuracy;

    public float? heading;

    public float? speed;

    public DateTimeOffset createdAt;

    public DateTimeOffset updatedAt;
}