using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    public GameObject bulletGO;
    public Transform originBullet;

    public float bulletForce;

    private GameObject tmpBullet;

    public void Fire(int bullets)
    {
       
        
            if (bullets > 0)
            {
                tmpBullet = Instantiate(bulletGO, originBullet.position, Quaternion.identity);

                tmpBullet.transform.up = originBullet.forward;

                tmpBullet.GetComponent<Rigidbody>().AddForce(originBullet.forward * bulletForce, ForceMode.Impulse);
                
                
            }
        
        
    }
}
