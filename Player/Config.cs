
using System;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class Config
{
    public string ServerHostname { get; }
    public int ServerPort { get; }
    public int TargetRoomId { get; }

    public int ShortpollingInterval { get; }

    public const string ConfigFileName = "TelemetryClientConfig.xml";
    public const string DefaultConfigFileName = "DefaultConfig";

    public Config(string configFilePath)
    {
        XmlDocument doc = new XmlDocument();
        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException(configFilePath);
        }

        try
        {
            Debug.Log("Loading config at: " + configFilePath);
            doc.Load(configFilePath);
        }
        catch (Exception e)
        {
            throw new Exception($"Error loading config file ({configFilePath}): {e}");
        }
        
        try
        {
            ServerHostname = GetNodeText("server/hostname");
            ServerPort = int.Parse(GetNodeText("server/port"));
            ShortpollingInterval = int.Parse(GetNodeText("shortpollingInterval"));
            TargetRoomId = int.Parse(GetNodeText("targetRoomId"));
            string GetNodeText(string node) => doc.DocumentElement.SelectSingleNode(node).InnerText;
        }
        catch (Exception e)
        {
            Debug.LogError("Config loading failed (potentially invalid config values): " + e);
            throw e;
        }
    }
    public static Config LoadConfig()
    {
        return new Config(Path.Combine(Application.streamingAssetsPath, ConfigFileName));
    }

    public override string ToString()
    {
        return "TelemetryClient Config:\n" +
               $"Server: {ServerHostname}:{ServerPort}\n" +
               $"Short-polling Interval: {ShortpollingInterval}ms\n" +
               $"Target Room Number: {TargetRoomId}";
    }
}
