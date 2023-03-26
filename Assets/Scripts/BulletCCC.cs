using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCCC : MonoBehaviour
{
    public GameObject explotion;

    public GameObject bulletDecal;

    private IEnumerator coroutine;

    private float timeToWaitForDestroy = 35.0f;
    private void OnCollisionEnter(Collision collision)
    {
        
        //informacion de 
        GameObject exp = Instantiate(explotion, collision.contacts[0].point, Quaternion.identity);
        Vector3 startPos = collision.contacts[0].point;
        Vector3 addV = collision.contacts[0].normal * 0.01f;
        Quaternion startRot = Quaternion.LookRotation(addV *-1);

        //coroutine = DestroyVFX(timeToWaitForDestroy,exp);
        //StartCoroutine(coroutine);

        //Instantiate(bulletDecal, startPos + addV, startRot);

        Destroy(exp, exp.GetComponent<ParticleSystem>().main.duration* timeToWaitForDestroy);

        //Debug.Log("::::" + collision.);

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("IF OnTriggerEnter:" + other.transform.tag);
        if (other.transform.CompareTag("Enemy"))
        {


            EnemyController currentEnemy = other.transform.parent.GetComponent<EnemyController>();
            currentEnemy.setDamageByExplosion();



        }

    }


    private IEnumerator DestroyVFX(float waitTime, GameObject vfxToDestroy)
    {
        while (true)
        {
            if (vfxToDestroy != null) {
                Destroy(vfxToDestroy);
            }
            yield return new WaitForSecondsRealtime(waitTime);
            print("DestroyVFX " + Time.time);
        }
    }
}
