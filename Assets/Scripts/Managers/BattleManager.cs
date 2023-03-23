using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public int counterMaxEnemiesDefeat = 1;

    public int counterEnemiesDefeat = 0;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CountEnemyDefeat() {
        counterEnemiesDefeat++;

    }

}
