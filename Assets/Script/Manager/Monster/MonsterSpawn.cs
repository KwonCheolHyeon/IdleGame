using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monsterPrefab; // 소환할 몬스터의 프리팹
    public int numberOfMonsters = 8; // 소환할 몬스터의 개수
    public float radius = 5f; // 플레이어로부터 몬스터를 소환할 반지름 거리

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void SpawnMonsters()
    {
        // 플레이어의 현재 위치
        Vector2 playerPosition = transform.position;

        // 원형으로 몬스터를 소환
        for (int i = 0; i < numberOfMonsters; i++)
        {
            // 각 몬스터의 각도 계산
            float angle = i * Mathf.PI * 2 / numberOfMonsters;
            // 각도에 따른 몬스터 위치 계산
            Vector2 monsterPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            // 플레이어 위치를 기준으로 오프셋 적용
            Vector2 spawnPosition = playerPosition + monsterPosition;
            // 몬스터 소환
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

}
