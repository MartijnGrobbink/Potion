using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //sources
    //https://www.youtube.com/watch?v=UjkSFoLxesw

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Animator animator;
    public HealthBar healthBar;

    public float maxhealth  ;
    public float health;
    public float attackdamage;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, enemyDeath;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        health = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);

        //check if player is in attack range or in sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange && !enemyDeath) Patroling();
        if(playerInSightRange && !playerInAttackRange && !enemyDeath) ChasePlayer();
        if(playerInSightRange && playerInAttackRange && !enemyDeath) AttackPlayer();
        if(enemyDeath) EnemyDeath();
    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //At Walkpoint
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }    

    private void AttackPlayer()
    {
        //Adding the attack

        //Making sure that the enemy doesn't move while attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            player.GetComponent<Health>().damage = attackdamage;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0) enemyDeath = true;
    }
    
    private void EnemyDeath()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y -transform.localScale.y / 2, transform.position.z);
        agent.SetDestination(transform.position);
        animator.SetTrigger("Death");
        Invoke(nameof(DestoryEnemy), 4f);
    }

    private void DestoryEnemy()
    {
        Destroy(gameObject);
    }
}
