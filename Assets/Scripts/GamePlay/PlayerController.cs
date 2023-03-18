using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    private AnimatorStateInfo animatorState;

    private AnimatorStateInfo animatorStateWave;
    private AnimatorStateInfo animatorStateAttack;

    public CapsuleCollider playerCollider;

    public Transform objectToGrab;

    private Transform objectToPunch;

    public Rigidbody rb;

    public float jumpAmount;

    private bool canPunch = true;

    public GameEvent dieEvent;
    public GameEvent restoreLifePointsEvent;
    public GameEvent respawnEvent;

    public Transform respawn;

    //private EnemyController enemyActive;

    //pprivate int counterAnimationAttack = 0;
    void Start()
    {
        
        playerAnimator = GetComponent<Animator>();
        
        //enemyActive = null;


    }

    private void Awake()
    {
        respawnEvent.Raise();
    }

    void Update()
    {
        animatorState = playerAnimator.GetCurrentAnimatorStateInfo(0);
        
        playerAnimator.SetFloat("speed", Input.GetAxis("Vertical"));
        playerAnimator.SetFloat("direction", Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space) && (animatorState.IsName("LocomotionRun") || animatorState.IsName("Idle"))) {
            
            playerAnimator.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            //AudioManager.instance.PlaySFX(SFXType.JUMP);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            playerAnimator.SetTrigger("wave");
        }
        //colliderHeight
        playerCollider.height = playerAnimator.GetFloat("colliderHeight");

        Physics.gravity = Vector3.up * playerAnimator.GetFloat("gravity");

        if (Input.GetKeyDown(KeyCode.G)) {
            playerAnimator.SetTrigger("grab");
        }

        if (canPunch && Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAnimator.SetTrigger("punch");

            if (objectToPunch != null) {
                Vector3 ver = new Vector3(objectToPunch.position.x, this.transform.position.y, objectToPunch.position.z);
                transform.LookAt(ver);
            }

            //counterAnimationAttack = 0;
        }
        else if(!canPunch && Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAnimator.SetTrigger("fire");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            canPunch = !(canPunch);
            //playerAnimator.SetTrigger("punch");
            //counterAnimationAttack = 0;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            playerAnimator.SetTrigger("dive");
            
        }

    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (objectToGrab != null && animatorStateWave.IsName("Grab"))
        {

            //cabeza
            playerAnimator.SetLookAtWeight(1);
            playerAnimator.SetLookAtPosition(objectToGrab.position);

            

            //mano
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, objectToGrab.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, objectToGrab.rotation);



        }else if (objectToPunch != null && animatorStateAttack.IsName("Punch"))
        {
            Vector3 temp = new Vector3(objectToPunch.position.x, this.transform.position.y + 1.75f, objectToPunch.position.z);
            //Debug.Log("Position y :" + objectToPunch.position.y);

            objectToPunch.position = temp;
            //punchPosition.z += 2f;
            //cabeza
            playerAnimator.SetLookAtWeight(1);
            playerAnimator.SetLookAtPosition(objectToPunch.position);

            //playerAnimator.bodyRotation = objectToPunch.rotation;

            //mano
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, objectToPunch.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, objectToPunch.rotation);

            /*
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            //playerAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, objectToPunch.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, objectToPunch.rotation);

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
            //playerAnimator.SetIKPosition(AvatarIKGoal.RightFoot, objectToPunch.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightFoot, objectToPunch.rotation);
            */
            /*
            if(enemyActive != null){
                Debug.Log("lifeEnemy:" + enemyActive.imgVida.fillAmount);

                if (couterAnimationAttack == 0) {
                    enemyActive.setValueLife();
                    couterAnimationAttack += 1;
                }
                
                Debug.Log("lifeEnemy:" + enemyActive.imgVida.fillAmount);
            }
            */
        }
        else {
            //cabeza
            playerAnimator.SetLookAtWeight(0);
            //mano
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Debug.Log("IF OnTriggerEnter:" + other.transform.tag);
            //this.objectToPunch = other.transform.FindChild("HeadEnemy").transform;
            this.objectToPunch = other.transform;
            
            
        }

        if (other.transform.CompareTag("Fall"))
        {
            dieEvent.Raise();
            restoreLifePointsEvent.Raise();
            this.transform.position = respawn.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            
            //Debug.Log("ThirdPersonController.OnTriggerExit Collider:");

            
            this.objectToPunch = null;
            //this.enemyActive = null;

        }
    }

    public void RespawnPlayer() {
        this.transform.position = respawn.position;
    }

    
}
