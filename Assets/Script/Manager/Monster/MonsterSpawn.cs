using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject playerObject;
    public int numberOfMonsters = 10; // �� ���� ��ȯ�� ������ ����
    public float minRadius = 10f; // �ּ� ��ȯ �ݰ�
    public float maxRadius = 15f; // �ִ� ��ȯ �ݰ�

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

            // �� ���� �ۿ��� ������ ��ġ ���
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minRadius, maxRadius);
            Vector2 spawnPosition = playerPosition + randomDirection * randomDistance;

            // ���� ��ġ�� Ȱ��ȭ ����
            monsterObject.transform.position = spawnPosition;
            monsterObject.SetActive(true);

            // ���� ��ũ��Ʈ ����
            EnemyScript monsterScript = monsterObject.transform.Find("UnitRoot").GetComponent<EnemyScript>();
            if (monsterScript != null)
            {
                monsterScript.gameObject.SetActive(true);
                monsterScript.MonsterSetting(stage);
            }
            else
            {
                Debug.LogError("EnemyScript�� ã�� �� �����ϴ�.");
            }
        }
    }

    public void BossSpawnMonsters(int monsterType, int stage)
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
            GameObject monsterObject = MonsterManager.Instance.BossGet(monsterType);

            if (monsterObject == null)
            {
                Debug.Log("Ȱ��ȭ ������ ���Ͱ� �����ϴ�.");
                return;
            }

            // �� ���� �ۿ��� ������ ��ġ ���
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minRadius, maxRadius);
            Vector2 spawnPosition = playerPosition + randomDirection * randomDistance;

            // ���� ��ġ�� Ȱ��ȭ ����
            monsterObject.transform.position = spawnPosition;
            monsterObject.SetActive(true);

            // ���� ��ũ��Ʈ ����
            EnemyScript monsterScript = monsterObject.transform.Find("UnitRoot").GetComponent<EnemyScript>();
            if (monsterScript != null)
            {
                monsterScript.gameObject.SetActive(true);
                monsterScript.BossSetting(stage);
            }
            else
            {
                Debug.LogError("EnemyScript�� ã�� �� �����ϴ�.");
            }
        }
    }
}
