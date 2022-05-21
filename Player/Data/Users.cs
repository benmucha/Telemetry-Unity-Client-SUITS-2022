using System;

[Serializable]
public class UsersList
{
    public UserData[] list;

    public static string FormattedJsonStringWrapper => "{{\"list\":{0}}}";
}

[Serializable]
public class UserData
{
    public int id;

    public string username;

    public int room;

    public DateTimeOffset createdAt;

    public DateTimeOffset updatedAt;
}

[Serializable]
public class UserInputModel
{
    public string username;

    public int room;
}