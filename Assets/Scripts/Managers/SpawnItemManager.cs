using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] itemsElements;


    public int startSpawnTime = 10;
    public int spawnTime = 5;
    public int spawPointLength;
    public int maxItems = 1;

    public int counterItems = 0;

    private 

    // Use this for initialization
    void Start()
    {

        InvokeRepeating("Spawn", startSpawnTime, spawnTime);
    }

    private void Update()
    {
        
    }

    void Spawn()
    {
        if (counterItems < maxItems)
        {
            counterItems++;
            int spawnPoints = Random.Range(0, spawPointLength);
            int randomNuke = Random.Range(0, itemsElements.Length);
            //Debug.Log(spawnPoints);
            //Debug.Log(randomNuke);
            //Debug.Log(this.enemies[randomNuke]);
            
            if (itemsElements != null && this.itemsElements[randomNuke] != null)
            {
                Instantiate(this.itemsElements[randomNuke], this.spawnPoints[spawnPoints].position, this.spawnPoints[spawnPoints].rotation);
            }

            //instan.SetActive(true);
        }
        
    }

    public void DiscountItem() {
        counterItems--;
    }
}
