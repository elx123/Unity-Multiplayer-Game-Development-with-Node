using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;

public class Player{
       public string id;
    }

    public class PlayerList
{
    public Player[] Players;
}

public class Network : MonoBehaviour
{
    public SocketIOUnity socket;    // Start is called before the first frame update
    
    public GameObject playerPrefab;
    
    Dictionary<string,GameObject> players;

    void Start()
    {
        Debug.Log("1");
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
                    
                    /*
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        Instantiate(playerPrefab);
                    });
                    */
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
