using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance;
    public static MonsterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonsterManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<MonsterManager>();
                    instance.Start(); // 수동으로 Start 메서드 호출
                }
            }
            return instance;
        }
    }

    public GameObject[] monsterPrefabs;
    public GameObject[] bossMonsterPrefabs;
    public List<GameObject>[] monsterPools;
    public int maxActiveMonsters = 10;
    public float monsterSpawnTime;
    public MonsterSpawn monsterSpawnScript;
    public bool bossSpawnOn;
    void Start()
    {
        monsterPools = new List<GameObject>[monsterPrefabs.Length];
        for (int index = 0; index < monsterPools.Length; index++)
        {
            monsterPools[index] = new List<GameObject>();

            // 풀 초기화: 각 풀에 maxActiveMonsters 개수만큼 미리 생성
            for (int i = 0; i < maxActiveMonsters; i++)
            {
                GameObject monster = Instantiate(monsterPrefabs[index], transform);
                monster.SetActive(false);
                monsterPools[index].Add(monster);
            }
        }
        monsterSpawnScript = GetComponent<MonsterSpawn>();
        monsterSpawnTime = 5.0f;

        bossSpawnOn = false;
    }

    public GameObject Get(int index)
    {
        if (index < 0 || index >= monsterPrefabs.Length)
        {
            return null;
        }

        if (monsterPrefabs[index] == null)
        {
            return null;
        }

        GameObject select = null;

        foreach (GameObject item in monsterPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                break;
            }
        }

        return select;
    }

    public GameObject BossGet(int index)
    {
        if (index < 0 || index >= bossMonsterPrefabs.Length)
        {
            return null;
        }

        if (bossMonsterPrefabs[index] == null)
        {
            return null;
        }

        GameObject select = null;

        select = bossMonsterPrefabs[index];

        return select;
    }

    public int GetActiveMonsterCount()
    {
        int activeCount = 0;
        foreach (var pool in monsterPools)
        {
            foreach (var monster in pool)
            {
                if (monster.activeSelf)
                {
                    activeCount++;
                }
            }
        }
        return activeCount;
    }

    void Update()
    {
        if(!bossSpawnOn) 
        {
            monsterSpawnTime -= Time.deltaTime;
            if (monsterSpawnTime <= 0)
            {
                Debug.Log("타이머 완료, 몬스터 소환 시도");
                MonsterSpawnMonsters();
            }
        }
    }

    public void MonsterSpawnMonsters()
    {
        int activeMonsterCount = GetActiveMonsterCount();

        Debug.Log("활성화된 몬스터 수: " + activeMonsterCount);

        if (activeMonsterCount >= maxActiveMonsters)
        {
            Debug.Log("활성화된 몬스터 수가 최대치에 도달했습니다. 소환 중지.");
            return; // 활성화된 몬스터가 maxActiveMonsters를 초과하면 소환 중지
        }

        int stageCount = GameManager.Instance.stageCount;
        int monsterIndex = (stageCount / 5) % monsterPrefabs.Length;

        Debug.Log("몬스터 소환 중... 몬스터 인덱스: " + monsterIndex);

        monsterSpawnScript.SpawnMonsters(monsterIndex, stageCount);
        monsterSpawnTime = 10.0f; // 타이머 초기화
    }

    public void BossSpawn() 
    {
        if (bossSpawnOn)
        {
            return;
        }
        else 
        {
            bossSpawnOn = true;
            int stageCount = GameManager.Instance.stageCount;
            int monsterIndex = (stageCount / 5) % monsterPrefabs.Length;
            monsterSpawnScript.BossSpawnMonsters(monsterIndex, stageCount);
        }
    }
}
