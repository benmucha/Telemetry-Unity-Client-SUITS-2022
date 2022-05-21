using System;

[Serializable]
public class LsarMessagesList
{
    public LsarMessageData[] list;

    public static string FormattedJsonStringWrapper => "{{\"list\":{0}}}";
}

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
[Serializable]
public class LsarMessageData
{
    public int id;

    public int room;

    public int sender;

    public bool isPaused;

    public string time;

    public string priority_tag;

    public string encoded_lat;

    public string encoded_lon;

    public string pnt_source;

    public string condition_state;

    public string vmc_txt;

    public string tac_sn;

    public string cntry_code;

    public string homing_dvc_stat;

    public string ret_lnk_stat;

    public string test_pronto;

    public string vessel_id;

    public string beacon_type;

    public DateTimeOffset createdAt;

    public DateTimeOffset updatedAt;
}
