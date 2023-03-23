using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagment : MonoBehaviour
{
    private PlayerData playerData;

    public GameEvent drawUIEvent;

    public GameEvent respawnPlayerEvent;

    public PlayerProfileManager profileManager;


    void Start()
    {
        Debug.Log("inicilizando star() en DataManagment...");
        try
        {
            playerData = profileManager.LoadDataCurrent();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
       
        Debug.Log("playerData is :" + playerData);
        if (playerData == null) {
            playerData = new PlayerData();
            playerData.lifes = 15;
            playerData.itmes = 0;
            playerData.currentLevel = 1;
            playerData.lifePoints = 100;
            playerData.counterEnemies = 0;
            playerData.counterItems = 0;

        }
           
        drawUIEvent.Raise();

    }

    public void RecieveDamage()
    {
        playerData.lifePoints -= 5;
        if (playerData.lifePoints <= 0) {
            this.Die();
            this.RestoreLifePoints();
            this.respawnPlayerEvent.Raise();
        }
        drawUIEvent.Raise();
    }

    public void TakeItem()
    {
        playerData.itmes++;
        drawUIEvent.Raise();
    }

    public void SetWorld(int index)
    {
        playerData.currentLevel = index;
        drawUIEvent.Raise();
    }

    public void Die() {
        playerData.lifePoints = 0;
        playerData.lifes--;
        drawUIEvent.Raise();
    }

    public void RestoreLifePoints() { 
        playerData.lifePoints = 100;
        drawUIEvent.Raise();
    }

    public void CounterEnemies()
    {
        playerData.counterEnemies++;
        drawUIEvent.Raise();
    }

    public void CounterItems()
    {
        playerData.counterItems++;
        drawUIEvent.Raise();
    }
}
