using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public GameObject player;

    public void OnClick(Vector3 postion)
    {
        var navPos = player.GetComponent<NavigatePosition>();
        var netMove = player.GetComponent<NetworkMove> ();
        
        navPos.NevigateTo(postion);
        netMove.OnMove(postion);
    }
}
