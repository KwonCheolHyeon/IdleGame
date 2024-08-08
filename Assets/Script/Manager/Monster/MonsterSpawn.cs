using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject playerObject;
    public int numberOfMonsters = 10; // 한 번에 소환할 몬스터의 개수
    public float radius = 5f; // 플레이어로부터 몬스터를 소환할 반지름 거리

    public void SettingPlayerObject()
    {
        playerObject = GameManager.Instance.player.gameObject;
        if (playerObject != null)
        {
            Debug.Log("SettingPlayerObject 있음");
        }
        else
        {
            Debug.Log("SettingPlayerObject 없음");
        }
    }

    public void SpawnMonsters(int monsterType, int stage)
    {
        SettingPlayerObject();

        if (playerObject == null)
        {
            Debug.LogError("PlayerObject가 설정되지 않았습니다.");
            return;
        }

        Vector2 playerPosition = playerObject.transform.position;

        for (int i = 0; i < numberOfMonsters; i++)
        {
            // MonsterManager에서 몬스터를 가져오기
            GameObject monsterObject = MonsterManager.Instance.Get(monsterType);

            if (monsterObject == null)
            {
                Debug.Log("활성화 가능한 몬스터가 없습니다.");
                return;
            }

            // 원형으로 몬스터 위치를 계산
            float angle = i * Mathf.PI * 2 / numberOfMonsters;
            Vector2 monsterPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 spawnPosition = playerPosition + monsterPosition;

            // 몬스터 위치와 활성화 설정
            monsterObject.transform.position = spawnPosition;
            monsterObject.SetActive(true);

            // 몬스터 스크립트 설정
            EnemyScript monsterScript = monsterObject.transform.Find("UnitRoot").GetComponent<EnemyScript>();
            if (monsterScript != null)
            {
                monsterScript.MonsterSetting(stage);
            }
            else
            {
                Debug.LogError("EnemyScript를 찾을 수 없습니다.");
            }
        }
    }
}
