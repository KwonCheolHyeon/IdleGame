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
        // LateUpdate는 모든 Update가 끝난 후에 호출되므로, 카메라가 플레이어를 따라가도록 보장합니다.
        if (player != null) 
        {
            transform.position = player.position + offset;
        }
    }
}
