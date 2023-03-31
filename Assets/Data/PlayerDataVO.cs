using System;
using UnityEngine;
[Serializable]
public class PlayerDataVO
{
    public int lifes;
    public int itmes;
    public int currentLevel;
    public int lifePoints;
    public string sceneName;
    public int counterEnemies;
    public int counterItems;
    public Vector3 playerPosition;
    public bool newGame;
    public int energyPoints;
    public bool canPunch;
    public bool isReturn;
    public bool isContinue;
    public bool isBattle;
}