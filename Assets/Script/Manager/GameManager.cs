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

        // Resources에서 프리팹을 로드
        GameObject playerPrefab = Resources.Load<GameObject>("SPUM/SPUM_Units/Unit000");
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = Quaternion.identity;

        // 프리팹 인스턴스화
        GameObject playerInstance = Instantiate(playerPrefab, spawnPosition, spawnRotation);
        if (playerInstance != null)
        {
            
            Transform childTransform = playerInstance.transform.Find("UnitRoot");
            if (childTransform != null)
            {
              
                player = childTransform.gameObject.GetComponent<PlayerScript>();

                // 카메라 설정
                Camera.main.GetComponent<CameraScript>().CameraSetting(childTransform.gameObject);
            }
            else
            {
                Debug.LogError("UnitRoot 자식 오브젝트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("GameManager: playerPrefab이 없습니다.");
        }




    }
}
