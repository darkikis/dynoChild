using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewEnemyTwoController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EnemyTwoController enemiCtrl = this.transform.parent.GetComponent<EnemyTwoController>();

            //Debug.Log(enemiCtrl.getCurrentState());

            if (enemiCtrl.getCurrentState() == EnemyState.PATROL)
            {
                enemiCtrl.setCurrentState(EnemyState.CHASE);
                enemiCtrl.cancelInvoke("GenerateRandomDestination");
                enemiCtrl.setPlayerTransform(other.transform);
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
                enemiCtrl.setCurrentState(EnemyState.PATROL);

                enemiCtrl.getEnemyAnimator().SetBool("attack", false);
                //enemiCtrl.getEnemyAnimator().SetFloat("attackF", 0.0f);

                //enemiCtrl.getEnemyAnimator().SetFloat("speed", enemiCtrl.getEnemyAgent().velocity.sqrMagnitude);
                enemiCtrl.invokeMethodName("GenerateRandomDestination");
                //CancelInvoke("GenerateRandomDestination");
            }

        }
    }
}
