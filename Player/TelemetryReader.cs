using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TelemetryReader
{
    private readonly int _shortPollInterval;
    private readonly ApiClient _apiClient;
    private CancellationTokenSource _pollingCancellationSource;

    private int TargetRoomId { get; set; }

    public delegate void ReceiveSimulationStateData(SimulationStateRoomData simulationStateRoom);
    /// <summary>
    /// Invoked when a SimulationState is received from the server.
    /// </summary>
    public event ReceiveSimulationStateData OnReceiveSimulationState;

    public delegate void ReceiveLsarData(List<LsarMessageData> lsarRoomData);
    /// <summary>
    /// Invoked when a SimulationState is received from the server.
    /// </summary>
    public event ReceiveLsarData OnReceiveLsar;

    public delegate void ApiError(Exception exception);
    /// <summary>
    /// Invoked when there is an error making an API request, or when the API gives an error response.
    /// </summary>
    public event ApiError OnApiError;
    
    /// <summary>
    /// Invoked when a GET request is made.
    /// </summary>
    public event ApiClient.GetRequestLog OnGetRequest
    {
        add => _apiClient.OnGetRequest += value;
        remove => _apiClient.OnGetRequest -= value;
    }
    /// <summary>
    /// Invoked when a POST request is made.
    /// </summary>
    public event ApiClient.PostRequestLog OnPostRequest
    {
        add => _apiClient.OnPostRequest += value;
        remove => _apiClient.OnPostRequest -= value;
    }
    
    public TelemetryReader(string hostname, int port, int targetRoomId, int shortPollInterval)
    {
        _shortPollInterval = shortPollInterval;
        TargetRoomId = targetRoomId;
        this._apiClient = new ApiClient(hostname, port);
    }
    
    //private static int ShortPollDelayMilliseconds(int tickRate) => (int)(1f / tickRate * 1000);

    public async void PostMessageToTargetRoom(string message, int userId)
    {
        var messageInputModel = new LsarMessageInputModel()
        {
            SenderId = userId,
            RoomId = TargetRoomId,
            Message = message
        };
        await _apiClient.PostReq("lsar", messageInputModel);
    }

    
    /// <summary>
    /// Starts reading from the API.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void StartReading()
    {
        if (_pollingCancellationSource != null)
        {
            throw new Exception("Stop this first !!");
        }
        
        _pollingCancellationSource = new CancellationTokenSource();
        ShortPollLoop(); // TaskCreationOptions.LongRunning
    }

    public async Task<List<UserData>> GetUsersInTargetRoom() => await GetUsersInRoom(TargetRoomId);
    public async Task<List<UserData>> GetUsersInRoom(int roomId)
    {
        return await _apiClient.GetObject<List<UserData>>(GetApiAddressWithRoom("users", roomId));
    }

    /// <summary>
    /// Polling loop for continuously polling data from the API.
    /// </summary>
    private async void ShortPollLoop()
    {
        CancellationToken cancellationToken = _pollingCancellationSource.Token;
        try
        {
            while (!cancellationToken.IsCancellationRequested)
                await Task.WhenAll(PollSimulationStateStep(), PollLsarStep(),
                    Task.Delay(_shortPollInterval, cancellationToken));
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Stopped polling");
        }
    }

    private async Task PollLsarStep()
    {
        try
        {
            var lsar = await _apiClient.GetObject<List<LsarMessageData>>(GetApiAddressWithTargetRoom("lsar"));
            OnReceiveLsar?.Invoke(lsar);
        }
        catch (Exception e)
        {
            OnApiError?.Invoke(e);
        }
    }

    private async Task PollSimulationStateStep()
    {
        try
        {
            var simulationState = await _apiClient.GetObject<List<SimulationStateRoomData>>(GetApiAddressWithTargetRoom("simulationstate"));
            OnReceiveSimulationState?.Invoke(simulationState.Count == 0 ? null : simulationState.First()); // Gets First because Telemetry JSON is always packed in an array even though it should only be one object per room.
        }
        catch (Exception e)
        {
            OnApiError?.Invoke(e);
        }
    }

    public void StopReading()
    {
        _pollingCancellationSource.Cancel();
        _pollingCancellationSource = null;
    }

    private string GetApiAddressWithTargetRoom(string apiAddress) => GetApiAddressWithRoom(apiAddress, TargetRoomId);
    private static string GetApiAddressWithRoom(string apiAddress, int roomId) => $"{apiAddress}/room/{roomId}";
}
