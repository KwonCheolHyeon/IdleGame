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

    public void CameraSetting() 
    {
        GameObject playerObject = GameObject.Find("Unit000");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("�÷��̾ ã�� ���Ͽ����ϴ�");
        }
        // �ʱ� �������� ���
        offset = transform.position - player.position;
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
