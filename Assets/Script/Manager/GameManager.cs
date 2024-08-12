using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 존재하지 않는다면 새로 생성
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();

                    // 수동으로 초기화 메서드 호출
                    instance.Start();
                }
            }
            return instance;
        }
    }

    public PlayerScript player { get; private set; }
    public int stageCount { get; private set; }
    public int money { get; private set; }

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI moneyText;
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


        stageCount = 1;
    }

    public void MonsterStageUp() 
    {
        stageCount += 1;
        stageText.text = "Stage : " + stageCount;
    }

    public bool SpendMoney(int useMoney) 
    {
        money -= useMoney;
        if (money < 0)
        {
            money += useMoney;
            return false;
        }
        else 
        {
            moneyText.text = "Money : " + money;
            return true;
        }
        
    }

    public void EarnMoney(int earn) 
    {
        money += earn;
        moneyText.text = "Money : " + money;
    }
}
