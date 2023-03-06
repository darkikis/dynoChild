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

        drawUIEvent.Raise();

    }

    public void RecieveDamage()
    {
        playerData.lifes--;
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
}
