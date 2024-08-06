using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerScript player;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        // Resources���� �������� �ε�
        GameObject playerPrefab = Resources.Load<GameObject>("SPUM/SPUM_Units/Unit000");
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = Quaternion.identity;

        // ������ �ν��Ͻ�ȭ
        GameObject playerInstance = Instantiate(playerPrefab, spawnPosition, spawnRotation);
        if (playerInstance != null)
        {
            
            Transform childTransform = playerInstance.transform.Find("UnitRoot");
            if (childTransform != null)
            {
              
                player = childTransform.gameObject.GetComponent<PlayerScript>();

                // ī�޶� ����
                Camera.main.GetComponent<CameraScript>().CameraSetting(childTransform.gameObject);
            }
            else
            {
                Debug.LogError("UnitRoot �ڽ� ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("GameManager: playerPrefab�� �����ϴ�.");
        }




    }
}
