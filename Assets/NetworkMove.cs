using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMove : MonoBehaviour
{
    public Network instance;
    public void OnMove (Vector3 position)
    {
        Debug.Log("sending postiion to node" + VectorToJson(position));
        instance.socket.Emit("move",VectorToJson(position));
        //instance.socket.EmitStringAsJSON("move",VectorToJson(position));
    }

    string VectorToJson (Vector3 vector)
    {
        return string.Format(@"((""x"":""{0}"", ""y"":""{1}""}}", vector.x, vector.z);
    }
}
