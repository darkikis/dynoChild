using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int lifes;
    public int itmes;
    public int currentLevel;
    public int lifePoints;
}
