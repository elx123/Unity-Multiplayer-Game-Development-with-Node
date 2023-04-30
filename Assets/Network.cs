using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;


[System.Serializable]
public class DataItem
{
    public string id;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"Items\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class Network : MonoBehaviour
{
    public SocketIOUnity socket;    // Start is called before the first frame update
    
    public GameObject playerPrefab;
    
    Dictionary<string,GameObject> players;

    List<DataItem> ParseJson(string json)
    {
        List<DataItem> dataItems = new List<DataItem>();
        DataItem[] dataArray = JsonHelper.FromJson<DataItem>(json);

        foreach (DataItem item in dataArray)
        {
            dataItems.Add(item);
            Debug.Log("ID: " + item.id);
        }
        return dataItems;
    }

    void Start()
    {
        players = new Dictionary<string,GameObject>();
        var uri = new Uri("http://localhost:3000");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };

        
        socket.On("spawn", (response) =>
        {
            try {
                    Debug.Log("spawn begin" + response.ToString());
                    List<DataItem> result = ParseJson(response.ToString());
                    Debug.Log(result.Count);

                    if (result.Count == 0)
                    {
                        return;
                    }
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        GameObject playertemp = Instantiate(playerPrefab);
                        players.Add(result[0].id,playertemp);
                        Debug.Log(players.Count);
                    });
                    Debug.Log("spawn end");
            } catch(Exception e)
            {
                Debug.Log(e);
            }
        }); 

        socket.On("move",(data) =>
        {
            try {
                    Debug.Log("player is moving" + data.ToString());
                    
            } catch(Exception e)
            {
                Debug.Log(e);
            }
        });

        socket.On("registered", (data) =>
        {
            Debug.Log(data);
        });
     /*
        socket.OnPing += (sender, e) =>
        {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        */
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        

        Debug.Log("Connecting...");
        socket.Connect();

    }

    void Update()
    {
        
    }
}
