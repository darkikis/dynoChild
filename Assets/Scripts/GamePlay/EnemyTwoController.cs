using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyTwoController : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Animator enemyAnimator;
    private Transform playerTransform;


    public Vector2 patrolRange;

    private EnemyState currenState;

    private Vector3 randomPosition;

    public float patrolTime;
    public float chaseTime;

    public GameEvent battleScenceEvent;

    private float lifeEnemy = 100f;

    public Slider enemySlider;

    public GameEvent loadCurrentEvent;

    public GameManager gameManager;

    public GameEvent saveStatsCurrentEvent;

    public PlayerData playerData;

    private AudioSource audioSourceAttack;
    private AudioSource audioSourceDie;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        currenState = EnemyState.PATROL;

        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        if (currenState == EnemyState.CHASE)
        {
            enemyAnimator.SetFloat("speed", 0f);

            if (playerTransform != null)
            {
                enemyAgent.SetDestination(playerTransform.position);
            }

        }

        enemyAnimator.SetFloat("speed", enemyAgent.velocity.sqrMagnitude);


    }

    private void Awake()
    {
        //transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);

        GameObject audioAttack = this.transform.Find("AudioAttack").gameObject;

        if (audioAttack != null)
        {
            audioSourceAttack = audioAttack.GetComponent<AudioSource>();

        }

        GameObject audioDie = this.transform.Find("AudioDie").gameObject;

        if (audioDie != null)
        {
            audioSourceDie = audioDie.GetComponent<AudioSource>();

        }
    }

    private void UpdateState()
    {
        //Debug.Log(currenState);
        switch (currenState)
        {
            case EnemyState.PATROL:
                InvokeRepeating("GenerateRandomDestination", 0f, patrolTime);
                break;
            case EnemyState.CHASE:
                InvokeRepeating("PlayerDestination", 0f, chaseTime);
                break;
        }
    }

    private void GenerateRandomDestination()
    {
        randomPosition = transform.position + new Vector3(UnityEngine.Random.Range(-patrolRange.x, patrolRange.x), 0f, UnityEngine.Random.Range(-patrolRange.y, patrolRange.y));

        try
        {
            enemyAgent.SetDestination(randomPosition);
        }
        catch (Exception e)
        {
            Debug.Log("GenerateRandomDestination:");
        }

        //Vector3 ver = new Vector3(randomPosition.x, this.transform.position.y, randomPosition.z);
        //transform.LookAt(ver);

        //Debug.Log("GenerateRandomDestination:");
    }

    private void PlayerDestination()
    {
        enemyAgent.SetDestination(playerTransform.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("EnemyController Collision with:" + other.transform.tag);
        if (other.transform.CompareTag("Player"))
        {


            this.playerTransform = other.transform;
            if (other.GetType() != typeof(BoxCollider))
            {
                if (currenState == EnemyState.PATROL)
                {
                    currenState = EnemyState.CHASE;

                    CancelInvoke("GenerateRandomDestination");
                }
            }

        }
    }

    public EnemyState getCurrentState()
    {
        return this.currenState;
    }

    public void setCurrentState(EnemyState currentState)
    {
        this.currenState = currentState;
    }

    public void cancelInvoke(string methodName)
    {
        CancelInvoke(methodName);
    }

    public Animator getEnemyAnimator()
    {
        return this.enemyAnimator;
    }

    public NavMeshAgent getEnemyAgent()
    {
        return this.enemyAgent;
    }

    public Transform getPlayerTransform()
    {
        return this.playerTransform;
    }

    public void invokeMethodName(string methodName)
    {
        InvokeRepeating(methodName, 0f, patrolTime);
    }

    public void setPlayerTransform(Transform playerTransformParam)
    {
        this.playerTransform = playerTransformParam;
    }

    public void setDamage()
    {
        //Debug.Log(this.lifeEnemy);
        this.lifeEnemy = this.lifeEnemy - 5;
        this.enemySlider.value = this.lifeEnemy;
        if (this.lifeEnemy <= 0)
        {
            this.audioSourceDie.Play();
            Destroy(this.gameObject, 0.5f);
            if (BattleManager.instance != null)
            {
                BattleManager.instance.CountEnemyDefeat();
                Debug.Log(SceneManager.GetActiveScene().name.ToUpper());


                if (SceneManager.GetActiveScene().name.ToUpper().Contains("BATTLE"))

                {
                    Debug.Log(SceneManager.GetActiveScene().name);



                    if (BattleManager.instance.counterEnemiesDefeat >= BattleManager.instance.counterMaxEnemiesDefeat)
                    {

                        loadCurrentEvent.Raise();


                    }
                }
            }


        }
    }

    public void setDamageByExplosion()
    {
        //Debug.Log(this.lifeEnemy);
        this.lifeEnemy = this.lifeEnemy - 20;
        this.enemySlider.value = this.lifeEnemy;
        if (this.lifeEnemy <= 0)
        {
            Destroy(this.gameObject);
            if (BattleManager.instance != null)
            {

                BattleManager.instance.CountEnemyDefeat();
                if (BattleManager.instance.counterEnemiesDefeat >= BattleManager.instance.counterMaxEnemiesDefeat)
                {
                    loadCurrentEvent.Raise();

                }
            }

        }
    }

    public GameEvent getBattleEvent()
    {
        return this.battleScenceEvent;
    }

    public AudioSource getAttackAudioSource()
    {
        return this.audioSourceAttack;
    }

    public AudioSource getDieAudioSource()
    {
        return this.audioSourceDie;
    }
}
