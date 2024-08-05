using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerScript player;

    private void Awake()
    {
        Instance = this;
        GameObject playerPrefab = Resources.Load<GameObject>("SPUM/SPUM_Units/Unit000");
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(playerPrefab, spawnPosition, spawnRotation);
        if (playerPrefab != null)
        {
            player = playerPrefab.AddComponent<PlayerScript>();
        }
        Camera.main.GetComponent<CameraScript>().CameraSetting();
    }
    private void Start()
    {
        
    }
}
