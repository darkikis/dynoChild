using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagment : MonoBehaviour
{
    public PlayerData playerData;

    public GameEvent drawUIEvent;
    // Start is called before the first frame update
    void Start()
    {
        playerData.lifes = 5;
        playerData.itmes = 0;
        playerData.currentLevel = 1;
        playerData.lifePoints = 100;

        drawUIEvent.Raise();

    }

    public void RecieveDamage()
    {
        playerData.lifePoints -= 5;
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
}
