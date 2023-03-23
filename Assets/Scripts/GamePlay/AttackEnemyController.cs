using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyController : MonoBehaviour
{
    public GameEvent drawUIEvent;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyCapCollController Collision with:" + other.gameObject.name);

        if (other.transform.CompareTag("Player"))
        {
            EnemyController enemiCtrl = this.transform.parent.GetComponent<EnemyController>();

            Debug.Log("Capsule Collider state:" + enemiCtrl.getCurrentState());

            if (enemiCtrl.getCurrentState() == EnemyState.CHASE){
                enemiCtrl.setCurrentState(EnemyState.ATTACK);

                enemiCtrl.getEnemyAnimator().SetBool("attack", true);
                enemiCtrl.getEnemyAnimator().SetFloat("attackF", 1.0f);
                enemiCtrl.cancelInvoke("GenerateRandomDestination");
                drawUIEvent.Raise();
                if (enemiCtrl.getBattleEvent() != null)
                {
                    PlayerProfileManager.instance.SaveDataCurrent();
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
            EnemyController enemiCtrl = this.transform.parent.GetComponent<EnemyController>();

            //Debug.Log("OnTriggerExit Collider state:" + enemiCtrl.getCurrentState());

            if (enemiCtrl.getCurrentState() == EnemyState.ATTACK)
            {
                enemiCtrl.setCurrentState(EnemyState.CHASE);

                enemiCtrl.getEnemyAnimator().SetBool("attack", false);
                enemiCtrl.getEnemyAnimator().SetFloat("attackF", 0.0f);
                if (enemiCtrl.getPlayerTransform() != null)
                {
                    enemiCtrl.getEnemyAgent().SetDestination(enemiCtrl.getPlayerTransform().position);
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
