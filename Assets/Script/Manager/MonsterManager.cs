using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsteranager : MonoBehaviour
{
    public static Monsteranager Instance;

    public GameObject[] monsterObjects;

    private int mStageCount;

    private MonsterSpawn mMonsterSpawnScript;
    public float monsterSpawnTime;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mMonsterSpawnScript = GetComponent<MonsterSpawn>();
    }


    void Update()
    {
        
    }
}
