using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;

    private AnimatorStateInfo animatorState;

    private AnimatorStateInfo animatorStateWave;
    private AnimatorStateInfo animatorStateAttack;

    EnemyController currentEnemy;
    EnemyTwoController currentEnemyTwo;

    private Transform objectToPunch;

    private bool canPunch = true;
    private AudioSource audioSourceStep;
    private AudioSource audioSourceKickPunch;
    private bool isMoving;
    private ShooterController shooterController;
    
    public CapsuleCollider playerCollider;
    public Transform objectToGrab;
    public Rigidbody rb;
    public float jumpAmount;
    public GameEvent dieEvent;
    public GameEvent restoreLifePointsEvent;
    public GameEvent respawnEvent;
    public GameEvent drawUIEvent;
    public Transform respawn;
    public PlayerData playerData;
    public GameEvent discountEnergyEvent;
    public GameEvent pauseEvent;



    void Start()
    {
        
        playerAnimator = GetComponent<Animator>();


    }

    private void Awake()
    {
        if (!playerData.newGame && !playerData.isBattle){
            this.transform.position = playerData.playerPosition;
        }
        else {
            respawnEvent.Raise();
        }
        canPunch = playerData.canPunch;
        drawUIEvent.Raise();
        
        GameObject audioStep = this.transform.Find("AudioStep").gameObject; 
        
        
        if (audioStep != null)
        {
            audioSourceStep = audioStep.GetComponent<AudioSource>();
            
        }

        GameObject audioAttack = this.transform.Find("AudioAttack").gameObject;

        if (audioAttack != null)
        {
            audioSourceKickPunch = audioAttack.GetComponent<AudioSource>();
            
        }

        GameObject shooterGO = this.transform.Find("Shooter").gameObject;
        if (shooterGO != null) {
            shooterController = shooterGO.GetComponent<ShooterController>();
        }

    }

    void Update()
    {
        animatorState = playerAnimator.GetCurrentAnimatorStateInfo(0);
        
        playerAnimator.SetFloat("speed", Input.GetAxis("Vertical"));


        PlaySoundSteps();

        playerAnimator.SetFloat("direction", Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space) && (animatorState.IsName("LocomotionRun") || animatorState.IsName("Idle"))) {
            
            playerAnimator.SetTrigger("jump");
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            
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

            if (objectToPunch != null)
            {
                Vector3 ver = new Vector3(objectToPunch.position.x, this.transform.position.y, objectToPunch.position.z);
                transform.LookAt(ver);

                if (currentEnemy != null)
                {
                    currentEnemy.setDamage();

                }
                if (currentEnemyTwo != null) {
                    currentEnemyTwo.setDamage();
                }
            }

            //counterAnimationAttack = 0;
            PlaySoundAttack(true);

        }
        else if (!canPunch && Input.GetKeyUp(KeyCode.Mouse0))
        {
            playerAnimator.SetTrigger("fire");
            
            if (shooterController != null) {
                shooterController.Fire(playerData.energyPoints);
                discountEnergyEvent.Raise();
            }
            
        }
        else {
            PlaySoundAttack(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            canPunch = !(canPunch);
            playerData.canPunch = canPunch;
            drawUIEvent.Raise();
            //playerAnimator.SetTrigger("punch");
            //counterAnimationAttack = 0;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            playerAnimator.SetTrigger("dive");
            
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseEvent.Raise();
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

            
            //mano
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, objectToPunch.position);
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, objectToPunch.rotation);

            
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
            this.objectToPunch = other.transform;
            this.currentEnemy = other.transform.parent.GetComponent<EnemyController>();

            this.currentEnemyTwo = other.transform.parent.GetComponent<EnemyTwoController>();
            Debug.Log(currentEnemyTwo);
            //Vector3 ver = new Vector3(objectToPunch.position.x, this.transform.position.y, objectToPunch.position.z);
            //transform.LookAt(ver);



        }

        if (other.transform.CompareTag("Fall"))
        {
            dieEvent.Raise();
            restoreLifePointsEvent.Raise();
            this.RespawnPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            
            this.objectToPunch = null;
            this.currentEnemy = null;
            this.currentEnemyTwo = null;

        }
    }

    public void RespawnPlayer() {
        this.transform.position = respawn.position;
    }

    public bool getCanPunch() {
        return this.canPunch;
    }

    private void PlaySoundSteps() {
        if (Input.GetAxis("Vertical") > 0)
            isMoving = true; // better use != 0 here for both directions
        else
            isMoving = false;
        if (isMoving && !audioSourceStep.isPlaying)
        {
            audioSourceStep.Play();
        }

        if (!isMoving)
        {
            audioSourceStep.Stop();
        }
    }


    private void PlaySoundAttack(bool isPunch)
    {
        
        if (isPunch && !audioSourceKickPunch.isPlaying)
        {
            audioSourceKickPunch.Play();
        }

        if (!isPunch)
        {
            audioSourceKickPunch.Stop();
        }
    }


}
