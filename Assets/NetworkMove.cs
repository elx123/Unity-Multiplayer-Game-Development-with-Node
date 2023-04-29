using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMove : MonoBehaviour
{
    public Network instance;
    public void OnMove (Vector3 position)
    {
        Debug.Log("sending postiion to node" + position);
        instance.socket.Emit("move");
    }
}
