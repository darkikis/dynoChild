using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBattleManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;


    public int startSpawnTime = 10;
    public int spawnTime = 5;
    public int spawPointLength;
    public int maxEnemies = 1;

    private int counterEnemies = 0;
    


    // Use this for initialization
    void Start()
    {
        
        InvokeRepeating("Spawn", startSpawnTime, spawnTime);
    }

    void Spawn()
    {
        if (counterEnemies < maxEnemies)
        {
            counterEnemies++;
            int spawnPoints = Random.Range(0, spawPointLength);
            int randomNuke = Random.Range(0, enemies.Length);
            Debug.Log(spawnPoints);
            Debug.Log(randomNuke);
            Debug.Log(this.enemies[randomNuke]);
            //GameObject instan =  
            Instantiate(this.enemies[randomNuke], this.spawnPoints[spawnPoints].position, this.spawnPoints[spawnPoints].rotation);
            //instan.SetActive(true);
        }
        else {
            CancelInvoke("Spawn");
        }

            
    }
}
