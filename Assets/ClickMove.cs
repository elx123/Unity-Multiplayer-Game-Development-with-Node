using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick(Vector3 postion)
    {
        var navPos = player.GetComponent<NavigatePosition>();
        navPos.NevigateTo(postion);
    }
}
