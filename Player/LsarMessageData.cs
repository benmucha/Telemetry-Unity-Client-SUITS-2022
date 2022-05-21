using System;

/// <summary>
/// <see cref="LsarMessageData"/> from deserialized JSON.
/// </summary>
[Serializable]
public class LsarMessageData
{
    public int id { get; set; }

    public int room { get; set; }

    public int sender { get; set; }

    public bool isPaused { get; set; }

    public string time { get; set; }

    public string priority_tag { get; set; }

    public string encoded_lat { get; set; }

    public string encoded_lon { get; set; }

    public string pnt_source { get; set; }

    public string condition_state { get; set; }

    public string vmc_txt { get; set; }

    public string tac_sn { get; set; }

    public string cntry_code { get; set; }

    public string homing_dvc_stat { get; set; }

    public string ret_lnk_stat { get; set; }

    public string test_pronto { get; set; }

    public string vessel_id { get; set; }

    public string beacon_type { get; set; }

    public DateTimeOffset createdAt { get; set; }

    public DateTimeOffset updatedAt { get; set; }
}
