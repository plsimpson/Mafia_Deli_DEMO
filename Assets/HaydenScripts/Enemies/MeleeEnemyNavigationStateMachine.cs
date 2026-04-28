using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyNavigationStateMachine : BaseCharacter
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] EnemyStates state;
    //Get a reference to a script with an enemy count float

    [SerializeField] private float attackRange = 2f;

    private float reloadTime;

    [SerializeField] private PlayerHealth playerHealth;
    //Get a reference to the player's health script

    [Header("Audio")]
    [SerializeField] private AudioClip attackSfx; // assignable in Inspector
    private AudioSource audioSource;

    public enum EnemyStates
    {
        Idle,
        Chasing,
        Attacking,
        Reload
    }

    private void Start()
    {
        // Ensure an AudioSource exists to play the assigned clip
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
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

        RaycastHit hit;

        Vector3 origin = transform.position + Vector3.up * 1f; // chest height
        Vector3 direction = transform.forward;
        float radius = 0.5f;

        // Play attack sound if assigned
        if (attackSfx != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSfx);
        }

        // Debug visualization
        Debug.DrawRay(origin, direction * attackRange, Color.red, 0.5f);

        if (Physics.SphereCast(origin, radius, direction, out hit, attackRange))
        {
            if (hit.collider.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage();
            }
        }

        state = EnemyStates.Reload;
        reloadTime = 1f;
    }

    private void UpdateChasing()
    {
        agent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) < 5)
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