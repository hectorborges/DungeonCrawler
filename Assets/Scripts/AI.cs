using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Ability ability;
    public float rotationSpeed;

    NavMeshAgent agent;
    Transform player;
    Animator animator;
    Health health;
    Collider collision;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        collision = GetComponent<Collider>();
        player = GameObject.Find("Player").transform;
        agent.SetDestination(player.position);
    }

    private void Update()
    {
        if(health.isDead)
        {
            agent.isStopped = true;
            collision.enabled = false;
            return;
        }

        if(Vector3.Distance(transform.position, player.position) <= agent.stoppingDistance)
            Attack();
        else
            Follow();
    }

    void Attack()
    {
        animator.SetBool("Moving", false);

        if (!ability.OnCooldown())
        {
            animator.SetBool("Attack", true);
            ability.ActivateAbility();
            ability.TriggerCooldown();
        }
        if(!ability.IsActive())
            RotateTowards();
    }

    private void RotateTowards()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void ResetAttack()
    {
        animator.SetBool("Attack", false);
        ability.DeActivateAbility();
    }

    void Follow()
    {
        agent.SetDestination(player.position);
        animator.SetBool("Moving", true);
    }
}
