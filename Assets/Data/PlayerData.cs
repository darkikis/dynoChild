using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/PlayerData")]
[Serializable]
public class PlayerData : ScriptableObject
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
    public bool isBattle;
    public int energyPoints;
    public bool canPunch;
    public bool isReturn;
    public bool isContinue;

}
