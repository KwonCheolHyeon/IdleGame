using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monsterPrefab; // ��ȯ�� ������ ������
    public int numberOfMonsters = 8; // ��ȯ�� ������ ����
    public float radius = 5f; // �÷��̾�κ��� ���͸� ��ȯ�� ������ �Ÿ�

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void SpawnMonsters()
    {
        // �÷��̾��� ���� ��ġ
        Vector2 playerPosition = transform.position;

        // �������� ���͸� ��ȯ
        for (int i = 0; i < numberOfMonsters; i++)
        {
            // �� ������ ���� ���
            float angle = i * Mathf.PI * 2 / numberOfMonsters;
            // ������ ���� ���� ��ġ ���
            Vector2 monsterPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            // �÷��̾� ��ġ�� �������� ������ ����
            Vector2 spawnPosition = playerPosition + monsterPosition;
            // ���� ��ȯ
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

}
