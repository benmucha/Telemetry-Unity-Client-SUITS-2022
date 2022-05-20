using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        //Debug.Log(config.ServerHostname);
        _telemetryReader = new TelemetryReader(config.ServerHostname, config.ServerPort, 1, config.ShortpollingInterval);
        _telemetryReader.OnReceiveSimulationState += OnReceiveSimulationState;
        _telemetryReader.OnReceiveLocations += OnReceiveLocations;
        _telemetryReader.OnReceiveLsar += OnReceiveLsar;
        _telemetryReader.OnApiError += OnApiError;
        _telemetryReader.OnGetRequest += OnApiGetRequest;
        var users = await _telemetryReader.GetUsersInTargetRoom();
        UserId = users.Last().Id;
        Debug.Log("user id set: " + UserId);
        _telemetryReader.StartReading();
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
    private static void OnReceiveLocations(List<LocationData> locations)
    {
        if (locations.Count == 0)
        {
            Debug.Log("no locations");
        }
        else
        {
            var firstLocation = locations.First();
            Debug.Log($"location: {firstLocation.Id} - {firstLocation.UserId}");
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
            Debug.Log($"simulation state: {simulationStateRoom.Id} - {simulationStateRoom.WaterTimeLeft}");
            //Debug.Log($"test: " + simulationStateRoom.SubPressure.Percent);
        }
    }

    /// <summary>
    /// Callback for receiving data.
    /// </summary>
    private static void OnReceiveLsar(List<LsarMessageData> lsarMessage)
    {
        if (lsarMessage.Count == 0)
        {
            Debug.Log("no lsar messages");
        }
        else
        {
            var firstMessage = lsarMessage.First();
            Debug.Log($"lsar: {firstMessage.Id} - {firstMessage.VmcText}");
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