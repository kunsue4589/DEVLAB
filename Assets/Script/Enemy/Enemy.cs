using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;
    Animator animator;
    public LayerMask whatIsGround, whatIsoPlayer;

    
    /// /Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    ///Attacking
    public float timeBetweenAttack;
    bool alreadyAttacked;

    ///Status
    public float sightRange, attackRange;
    bool inSightRange, inAttackRange;


    // Start is called before the first frame update
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude);
        inSightRange = Physics.CheckSphere(transform.position, sightRange,whatIsoPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsoPlayer);

        if(!inSightRange&& !inAttackRange )
        {
            Patroling();
        }
        if (inSightRange && !inAttackRange)
        {
            ChasePlayer();
        }
        if (inSightRange && inAttackRange)
        {
            AttackPlayer();
        }
       
        
    }

    void Patroling()
    {
        if (!walkPointSet) SerchWalkPoint();
        
        if(walkPointSet) 
        {
        agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if( distanceToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }
    }
    void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
      
    }
    void AttackPlayer() 
    {
        animator.SetTrigger("attack");
      


        agent.SetDestination(transform.position);
        //transform.LookAt(playerTransform);

       

        if(!alreadyAttacked)
        {

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttack);
            Debug.Log("Player has been attacked");
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void SerchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x+randomX,transform.position.y,transform.position.z+randomZ);
        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround)) 
        {
        walkPointSet = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        //Draw a mock-up of the attack range.
        Gizmos.color = Color.red;//color
        Gizmos.DrawWireSphere(transform.position,sightRange);//WireSphere based on the attack position and attack range.


        //Draw a mock-up of the ground check range.
        Gizmos.color = Color.white;//color
        Gizmos.DrawWireSphere(transform.position,attackRange);//WireSphere based on transform.position and groundCheckRadius
    }
}
