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
            Debug.LogError("�÷��̾ ã�� ���Ͽ����ϴ�");
        }
    }

    void LateUpdate()
    {
        // LateUpdate�� ��� Update�� ���� �Ŀ� ȣ��ǹǷ�, ī�޶� �÷��̾ ���󰡵��� �����մϴ�.
        if (player != null) 
        {
            transform.position = player.position + offset;
        }
    }
}
