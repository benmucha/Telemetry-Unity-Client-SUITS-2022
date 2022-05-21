using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ApiClient
{
    private readonly Uri _baseApiUri;
    private readonly HttpClient _httpClient;

    public delegate void GetRequestLog(string method, string url);
    public event GetRequestLog OnGetRequest;
    public delegate void PostRequestLog(string method, string url, string content);
    public event PostRequestLog OnPostRequest;

    public ApiClient(string hostname, int port)
    {
        this._baseApiUri = new Uri($"http://{hostname}:{port.ToString()}/api/");
        this._httpClient = new HttpClient();
    }

    public async Task<T> GetObject<T>(string wrapperFormat, string apiAddress) =>
        JsonUtility.FromJson<T>(string.Format(wrapperFormat, await GetReq(apiAddress)));

    private async Task<string> GetReq(string apiAddress)
    {
        string url = GetApiUrl(apiAddress);
        OnGetRequest?.Invoke("GET", url);
        string result = await _httpClient.GetStringAsync(GetApiUrl(url));
        //Debug.Log("GetReq result: " + result);
        return result;
    }

    public async Task<HttpResponseMessage> PostReq<T>(string apiAddress, T bodyObject)
    {
        string url = GetApiUrl(apiAddress);
        string content = JsonUtility.ToJson(bodyObject);
        OnPostRequest?.Invoke("POST", url, content);
        return await _httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
    }

    //public async Task<string> Post(string apiAddress, NameValueCollection data = null) => Encoding.UTF8.GetString(await MakeApiReq("POST", apiAddress, async url => _httpClient.PostAsync(url, null))); // TODO

    /*
    private async Task<T> MakeApiReq<T>(string method, string apiAddress, Func<WebClient, string, Task<T>> internalRequestMethod)
    {
        string url = GetApiUrl(apiAddress);
        if (LogAllApiCalls)
            Debug.Log($"<color=orange>{method}</color> <color=default>{url}</color>");
        using WebClient webClient = new WebClient();
        var t = internalRequestMethod(webClient, url);
        if (await Task.WhenAny(t, Task.Delay(1000)) == t) return await t;
        webClient.CancelAsync();
        throw new Exception("API Request timed out");
    }
    */

    private string GetApiUrl(string apiAddress) => new Uri(_baseApiUri, apiAddress).ToString();
}
