using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    public GameEvent updateEnergyEvent;
    public GameEvent updateCountItems;
     
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyCapCollController Collision with:" + other.gameObject.name);

        if (other.transform.CompareTag("Player"))
        {

            if (other.GetType() == typeof(CapsuleCollider)) {
                updateEnergyEvent.Raise();
                updateCountItems.Raise();
                Destroy(this.gameObject);
            }
            

        }

    }
}