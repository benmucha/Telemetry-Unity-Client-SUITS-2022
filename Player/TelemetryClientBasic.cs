using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Example TelemetryClient script for implementing the Unity TelemetryClient package.
/// </summary>
public class TelemetryClientBasic : MonoBehaviour
{
    private TelemetryReader _telemetryReader;
    private int UserId { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        StartReader(Config.LoadConfig());
    }

    /// <summary>
    /// Starts reading simulation state data.
    /// </summary>
    /// <param name="config"></param>
    private async void StartReader(Config config)
    {
        Debug.Log("Starting Example TelemetryClient with config:\n" + config);
        _telemetryReader = new TelemetryReader(config.ServerHostname, config.ServerPort, config.TargetRoomId, config.ShortpollingInterval);
        _telemetryReader.OnReceiveSimulationState += OnReceiveSimulationState;
        _telemetryReader.OnReceiveLocations += OnReceiveLocations;
        _telemetryReader.OnReceiveLsar += OnReceiveLsar;
        _telemetryReader.OnApiError += OnApiError;
        _telemetryReader.OnGetRequest += OnApiGetRequest;
        var users = await _telemetryReader.GetUsersInTargetRoom();
        UserId = users.list.Last().id;
        Debug.Log("user id set: " + UserId);
        //_telemetryReader.StartReading();

        string teststr = @"{""Items"": [{""id"":1,""user"":5,""room"":1,""latitude"":0,""longitude"":0,""altitude"":0,""accuracy"":null,""altitudeAccuracy"":null,""heading"":0,""speed"":0,""createdAt"":""2022 - 05 - 20T21: 03:22.284Z"",""updatedAt"":""2022 - 05 - 20T21: 03:22.284Z""}]}";
        //LocationsList l = JsonUtility.FromJson<LocationsList>(string.Format(LocationsList.FormattedJsonStringWrapper, teststr));
        LocationData[] l = JsonHelper.FromJson<LocationData>(teststr);
        Debug.Log("TEST: " + l.Length + " - " + l[0].id);

        /*
        string jsonString = "{\r\n    \"Items\": [\r\n        {\r\n            \"playerId\": \"8484239823\",\r\n            \"playerLoc\": \"Powai\",\r\n            \"playerNick\": \"Random Nick\"\r\n        },\r\n        {\r\n            \"playerId\": \"512343283\",\r\n            \"playerLoc\": \"User2\",\r\n            \"playerNick\": \"Rand Nick 2\"\r\n        }\r\n    ]\r\n}";

        Player[] player = JsonHelper.FromJson<Player>(jsonString);
        Debug.Log(player[0].playerLoc);
        Debug.Log(player[1].playerLoc);*/
    }

    [Serializable]
    public class Player
    {
        public string playerId { get; set; }
        public string playerLoc { get; set; }
        public string playerNick { get; set; }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _telemetryReader.PostMessageToTargetRoom("worked", UserId);
        }
    }

    /// <summary>
    /// Callback for receiving data.
    /// </summary>
    private static void OnReceiveLocations(LocationsList locations)
    {
        Debug.Log("loc: " + locations.list.Length);
        if (locations.list.Length == 0)
        {
            Debug.Log("no locations");
        }
        else
        {
            Debug.Log("loct: " + locations.list[0].id);
            var firstLocation = locations.list.First();
            Debug.Log($"location: {firstLocation.id} - {firstLocation.user}");
        }
    }

    /// <summary>
    /// Callback for receiving data.
    /// </summary>
    private static void OnReceiveSimulationState(SimulationStateRoomData simulationStateRoom)
    {
        if (simulationStateRoom is null)
        {
            Debug.Log("simulation state is null");
        }
        else
        {
            Debug.Log($"simulation state: {simulationStateRoom.id} - {simulationStateRoom.t_water}");
            //Debug.Log($"test: " + simulationStateRoom.SubPressure.Percent);
        }
    }

    /// <summary>
    /// Callback for receiving data.
    /// </summary>
    private static void OnReceiveLsar(LsarMessagesList lsarMessage)
    {
        if (lsarMessage.list.Length == 0)
        {
            Debug.Log("no lsar messages");
        }
        else
        {
            var firstMessage = lsarMessage.list.First();
            Debug.Log($"lsar: {firstMessage.id} - {firstMessage.vmc_txt}");
        }
    }

    private static void OnApiError(Exception e)
    {
        Debug.LogException(e);
    }
    
    private void OnApiGetRequest(string method, string url)
    {
        Debug.Log($"<color=orange>{method}</color> <color=default>{url}</color>");
    }

    /// <summary>
    /// Stops reading data.
    /// </summary>
    private void StopReader()
    {
        if (_telemetryReader is null) 
            return;
        
        _telemetryReader.OnReceiveSimulationState -= OnReceiveSimulationState;
        _telemetryReader.OnReceiveLsar -= OnReceiveLsar;
        _telemetryReader.OnApiError -= OnApiError;
        _telemetryReader.StopReading();
    }
    
    
    private void OnDestroy()
    {
        StopReader();
    }
}