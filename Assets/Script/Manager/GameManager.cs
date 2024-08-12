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
            // �ν��Ͻ��� �������� �ʴ´ٸ� ���� ����
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();

                    // �������� �ʱ�ȭ �޼��� ȣ��
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
