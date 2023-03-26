using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    public GameObject bullet;
    public Transform originBullet;

    public float bulletForce;

    public int maxBullet = 6;

    private GameObject tmpBullet;

    PlayerController playerController;

    

    private void Awake()
    {
        playerController = this.transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerController != null && !playerController.getCanPunch())
        {

        
            if (Input.GetMouseButtonDown(0))
            {

                if (maxBullet > 0)
                {
                    tmpBullet = Instantiate(bullet, originBullet.position, Quaternion.identity);

                    tmpBullet.transform.up = originBullet.forward;

                    tmpBullet.GetComponent<Rigidbody>().AddForce(originBullet.forward * bulletForce, ForceMode.Impulse);
                    maxBullet--;
                    //Debug.Log("Balas disponibles: " + maxBullet);
                }

            }
        }
    }
}
