using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyNavigationStateMachine : BaseCharacter
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] EnemyStates state;
    //Get a reference to a script with an enemy count float

    private float reloadTime;

    public enum EnemyStates
    {
        Idle,
        Chasing,
        Attacking,
        Reload
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyStates.Idle:
                UpdateIdle();
                break;
            case EnemyStates.Chasing:
                UpdateChasing();
                break;
            case EnemyStates.Attacking:
                UpdateAttacking();
                break;
            case EnemyStates.Reload:
                UpdateReload();
                break;
        }
    }

    private void UpdateReload()
    {
        reloadTime -= Time.deltaTime;
        if (reloadTime < 0)
        {
            state = EnemyStates.Idle;
        }
    }

    private void UpdateAttacking()
    {
        Debug.Log("Attack");
        state = EnemyStates.Reload;
        reloadTime = 1f;
    }

    private void UpdateChasing()
    {
        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) < 2)
        {
            state = EnemyStates.Attacking;
        }
    }

    private void UpdateIdle()
    {
        if (Vector3.Distance(transform.position, target.position) < 30)
        {
            state = EnemyStates.Chasing;
        }
    }

    protected override void Die() // Overrides the base Die method
    {
        Debug.Log("Enemy Destroyed!");
        base.Die(); // Calls the base character's Die method as well
        Destroy(gameObject);
        //Lower the enemy count in the external script by one
    }
}