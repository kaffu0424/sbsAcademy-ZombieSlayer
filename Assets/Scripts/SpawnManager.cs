using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPrefab;
    public GameManager gameManager;

    private void Start()
    {
        InvokeRepeating("Spawn0", 3, 3);
        InvokeRepeating("Spawn1", 4, 9);
    }

    void Spawn0()
    {
        if(gameManager.GS == GameManager.GameStatus.Playing)
        {
            Instantiate(spawnPrefab[0], new Vector3(10, 0.95f, 0), Quaternion.identity);
        }
        
    }

    void Spawn1()
    {
        if (gameManager.GS == GameManager.GameStatus.Playing)
        {
            Instantiate(spawnPrefab[1], new Vector3(10, 0.95f, 0), Quaternion.identity);
        }
    }

}
