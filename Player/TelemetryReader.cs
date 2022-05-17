using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class TelemetryReader
{
    private readonly int _shortPollInterval;
    private readonly ApiClient _apiClient;
    private CancellationTokenSource _pollingCancellationSource;

    public delegate void ReceiveSimulationStateData(SimulationStateRoomData simulationStateRoom);
    /// <summary>
    /// Invoked when a SimulationState is received from the server.
    /// </summary>
    public event ReceiveSimulationStateData OnReceiveSimulationState;

    public delegate void ReceiveLsarData(LsarRoomData lsarRoom);
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
    
    public TelemetryReader(string hostname, int port, int shortPollInterval)
    {
        _shortPollInterval = shortPollInterval;
        this._apiClient = new ApiClient(hostname, port);
    }
    
    //private static int ShortPollDelayMilliseconds(int tickRate) => (int)(1f / tickRate * 1000);
    
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

    /// <summary>
    /// Polling loop for continuously polling data from the API.
    /// </summary>
    private async void ShortPollLoop()
    {
        CancellationToken cancellationToken = _pollingCancellationSource.Token;
        while (!cancellationToken.IsCancellationRequested)
            await Task.WhenAll(PollSimulationStateStep(), PollLsarStep(), Task.Delay(_shortPollInterval, cancellationToken));
    }

    private async Task PollLsarStep()
    {
        try
        {
            var lsar = await _apiClient.GetObject<List<LsarRoomData>>("lsar");
            OnReceiveLsar?.Invoke(lsar.First());
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
            var simulationState = await _apiClient.GetObject<List<SimulationStateRoomData>>("simulationstate");
            OnReceiveSimulationState?.Invoke(simulationState.First());
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
}
