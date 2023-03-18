using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Animator enemyAnimator;
    public Transform playerTransform;

    public Vector2 patrolRange;

    private EnemyState currenState;

    private Vector3 randomPosition;

    public float patrolTime;
    public float chaseTime;

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

            enemyAgent.SetDestination(playerTransform.position);
        }

        enemyAnimator.SetFloat("speed", enemyAgent.velocity.sqrMagnitude);

        
    }

    private void UpdateState()
    {
        Debug.Log(currenState);
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
        randomPosition = transform.position + new Vector3(Random.Range(-patrolRange.x, patrolRange.x), 0f, Random.Range(-patrolRange.y, patrolRange.y));


        enemyAgent.SetDestination(randomPosition);

        Debug.Log("GenerateRandomDestination:");
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
            if (other.GetType() != typeof(BoxCollider)) {
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

    public void setValueLife()
    {
        
    }
}

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
};
