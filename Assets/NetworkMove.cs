using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMove : MonoBehaviour
{
    struct PostionVector{
       public float x,y;
    }
    public Network instance;
    public void OnMove (Vector3 position)
    {
        string jsonString = JsonUtility.ToJson(VectorToJson(position));
        Debug.Log("sending postiion to node" +jsonString);
        //instance.socket.Emit("move",jsonString);
        instance.socket.EmitStringAsJSON("move",jsonString);
    }

    PostionVector VectorToJson (Vector3 vector)
    {
        //return string.Format(@"((""x"":""{0}"", ""y"":""{1}""}}", vector.x, vector.z);
        PostionVector result = new PostionVector();
        result.x = vector.x;
        result.y = vector.z;
        return result;
    }
}
