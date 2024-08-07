using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float monsterHealthPoint;
    
    void Start()
    {
        monsterHealthPoint = 10;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float playerAp) 
    {
        monsterHealthPoint -= playerAp;
        if (monsterHealthPoint <= 0)
        {
            Death();
        }

    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
