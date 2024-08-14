using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;  
    public Vector3 offset;    
    void Start()
    {
    }

    public void CameraSetting(GameObject playerObj) 
    {
        if (playerObj != null)
        {
            player = playerObj.transform;
            offset = transform.position - playerObj.transform.position;
        }
        else
        {
            Debug.LogError("플레이어를 찾지 못하였습니다");
        }
    }

    void LateUpdate()
    {
        
        if (player != null) 
        {
            transform.position = player.position + offset;
        }
    }
}
