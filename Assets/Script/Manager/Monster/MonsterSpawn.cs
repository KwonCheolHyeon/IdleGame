using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject playerObject;
    public int numberOfMonsters = 10; // �� ���� ��ȯ�� ������ ����
    public float radius = 5f; // �÷��̾�κ��� ���͸� ��ȯ�� ������ �Ÿ�

    public void SettingPlayerObject()
    {
        playerObject = GameManager.Instance.player.gameObject;
        if (playerObject != null)
        {
            Debug.Log("SettingPlayerObject ����");
        }
        else
        {
            Debug.Log("SettingPlayerObject ����");
        }
    }

    public void SpawnMonsters(int monsterType, int stage)
    {
        SettingPlayerObject();

        if (playerObject == null)
        {
            Debug.LogError("PlayerObject�� �������� �ʾҽ��ϴ�.");
            return;
        }

        Vector2 playerPosition = playerObject.transform.position;

        for (int i = 0; i < numberOfMonsters; i++)
        {
            // MonsterManager���� ���͸� ��������
            GameObject monsterObject = MonsterManager.Instance.Get(monsterType);

            if (monsterObject == null)
            {
                Debug.Log("Ȱ��ȭ ������ ���Ͱ� �����ϴ�.");
                return;
            }

            // �������� ���� ��ġ�� ���
            float angle = i * Mathf.PI * 2 / numberOfMonsters;
            Vector2 monsterPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 spawnPosition = playerPosition + monsterPosition;

            // ���� ��ġ�� Ȱ��ȭ ����
            monsterObject.transform.position = spawnPosition;
            monsterObject.SetActive(true);

            // ���� ��ũ��Ʈ ����
            EnemyScript monsterScript = monsterObject.transform.Find("UnitRoot").GetComponent<EnemyScript>();
            if (monsterScript != null)
            {
                monsterScript.MonsterSetting(stage);
            }
            else
            {
                Debug.LogError("EnemyScript�� ã�� �� �����ϴ�.");
            }
        }
    }
}
