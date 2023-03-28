using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject explotion;

    public PlayerData playerData;

    
    private AudioSource audioSourceImpact;
    private float timeToWaitForDestroy = 35.0f;

    private void Awake()
    {
        GameObject audioGOImpact = this.transform.Find("AudioImpact").gameObject;

        if (audioGOImpact != null)
        {
            audioSourceImpact = audioGOImpact.GetComponent<AudioSource>();
            
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        
        GameObject exp = Instantiate(explotion, collision.contacts[0].point, Quaternion.identity);
        if (!audioSourceImpact.isPlaying) {
            audioSourceImpact.Play();
        }
        
        Destroy(exp, exp.GetComponent<ParticleSystem>().main.duration* timeToWaitForDestroy);
        
        Destroy(this.gameObject, 1.0f);

        
    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("IF OnTriggerEnter:" + other.transform.tag);
        if (other.transform.CompareTag("Enemy"))
        {


            EnemyController currentEnemy = other.transform.parent.GetComponent<EnemyController>();
            if (currentEnemy != null) {
                currentEnemy.setDamageByExplosion();
            }

            EnemyTwoController currentEnemyTwo = other.transform.parent.GetComponent<EnemyTwoController>();
            if (currentEnemyTwo != null)
            {
                currentEnemyTwo.setDamageByExplosion();
            }

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
