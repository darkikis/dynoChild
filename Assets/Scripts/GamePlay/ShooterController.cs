using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    public GameObject bullet;
    public Transform originBullet;

    public float bulletForce;

    private int maxBullet = 0;

    private GameObject tmpBullet;

    PlayerController playerController;

    

    private void Awake()
    {
        playerController = this.transform.parent.GetComponent<PlayerController>();
        maxBullet = playerController.playerData.energyPoints;
    }

    // Update is called once per frame
    void Update()
    {
        maxBullet = playerController.playerData.energyPoints;
        if (playerController != null && !playerController.getCanPunch())
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
