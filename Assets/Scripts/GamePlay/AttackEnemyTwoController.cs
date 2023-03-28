using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyTwoController : MonoBehaviour
{
    public GameEvent receiveDamageEvent;
    public GameEvent saveDataCurrentEvent;
    public GameEvent receiveDamagePlayEvent;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyCapCollController Collision with:" + other.gameObject.name);

        if (other.transform.CompareTag("Player"))
        {
            EnemyTwoController enemiCtrl = this.transform.parent.GetComponent<EnemyTwoController>();

            //Debug.Log("Capsule Collider state:" + enemiCtrl.getCurrentState());

            if (enemiCtrl.getCurrentState() == EnemyState.CHASE){
                enemiCtrl.setCurrentState(EnemyState.ATTACK);

                enemiCtrl.getEnemyAnimator().SetBool("attack", true);
                //enemiCtrl.getEnemyAnimator().SetFloat("attackF", 1.0f);
                enemiCtrl.cancelInvoke("GenerateRandomDestination");
                enemiCtrl.getAttackAudioSource().Play();
                receiveDamageEvent.Raise();
                receiveDamagePlayEvent.Raise();
                if (enemiCtrl.getBattleEvent() != null)
                {
                    saveDataCurrentEvent.Raise();
                    enemiCtrl.getBattleEvent().Raise();
                }
                //CancelInvoke("GenerateRandomDestination");
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EnemyTwoController enemiCtrl = this.transform.parent.GetComponent<EnemyTwoController>();

            //Debug.Log("OnTriggerExit Collider state:" + enemiCtrl.getCurrentState());

            if (enemiCtrl.getCurrentState() == EnemyState.ATTACK)
            {
                enemiCtrl.setCurrentState(EnemyState.CHASE);

                enemiCtrl.getEnemyAnimator().SetBool("attack", false);
                //enemiCtrl.getEnemyAnimator().SetFloat("attackF", 0.0f);
                if (enemiCtrl.getPlayerTransform() != null)
                {
                    try
                    {
                        enemiCtrl.getEnemyAgent().SetDestination(enemiCtrl.getPlayerTransform().position);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                    
                }
                else {
                    enemiCtrl.invokeMethodName("GenerateRandomDestination");
                }
                
                //enemiCtrl.getEnemyAnimator().SetFloat("speed", enemiCtrl.getEnemyAgent().velocity.sqrMagnitude);
                //CancelInvoke("GenerateRandomDestination");
            }

        }
    }
}
