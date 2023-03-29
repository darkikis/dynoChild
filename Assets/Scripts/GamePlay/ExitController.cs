using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    public GameEvent showExitPanelEvent;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyCapCollController Collision with:" + other.gameObject.name);

        if (other.transform.CompareTag("Player"))
        {

            if (other.GetType() == typeof(CapsuleCollider))
                showExitPanelEvent.Raise();

        }

    }
}
