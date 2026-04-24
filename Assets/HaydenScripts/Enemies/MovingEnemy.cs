using Game;
using UnityEngine;

public class MovingEnemy : BaseCharacter
{
    public float minX = 2f;
    public float maxX = 3f;

    public float maxZ = 0f;
    public float minZ = 0f;

    public float distanceOffsetX = 3f;
    public float distanceOffsetZ = 0f;

    private float xTransform;
    private float zTransform;

    public Transform playerTransform;

    [SerializeField] private BulletTrails bulletTrails;
    [SerializeField] private Transform shootOrigin; // where bullets come from
    [SerializeField] private float attackRange = 50f;

    [Header("Combat")]
    [SerializeField] private float detectionRange = 30f;
    [SerializeField] private float aimTime = 1f;
    [SerializeField] private float reloadTime = 2f;

    private float aimTimer;
    private float reloadTimer;

    private Vector3 storedPlayerPosition;

    private enum EnemyState
    {
        Idle,
        Aiming,
        Attacking,
        Reloading
    }

    private EnemyState state;

    void Start()
    {
        minX = transform.position.x;
        maxX = transform.position.x + distanceOffsetX;

        minZ = transform.position.z;
        maxZ = transform.position.z + distanceOffsetZ;

        state = EnemyState.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;

            case EnemyState.Aiming:
                UpdateAiming();
                break;

            case EnemyState.Attacking:
                UpdateAttacking();
                break;

            case EnemyState.Reloading:
                UpdateReloading();
                break;
        }
    }

    private void HandleMovement()
    {
        if (distanceOffsetX > 0f)
        {
            xTransform = Mathf.PingPong(Time.time * speed, maxX - minX) + minX;
        }
        else
        {
            xTransform = transform.position.x;
        }

        if (distanceOffsetZ > 0f)
        {
            zTransform = Mathf.PingPong(Time.time * speed, maxZ - minZ) + minZ;
        }
        else
        {
            zTransform = transform.position.z;
        }

        transform.position = new Vector3(
            xTransform,
            transform.position.y,
            zTransform
        );
    }

    private void UpdateIdle()
    {
        HandleMovement();

        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance < detectionRange)
            {
                state = EnemyState.Aiming;
                aimTimer = aimTime;
            }
        }
    }

    private void UpdateAiming()
    {
        HandleMovement();

        if (playerTransform == null) return;

        // Look at player while aiming
        transform.LookAt(playerTransform);

        // Store player's position before waiting to fire
        storedPlayerPosition = playerTransform.position;

        aimTimer -= Time.deltaTime;

        if (aimTimer <= 0f)
        {
            state = EnemyState.Attacking;
        }
    }

    private void UpdateAttacking()
    {
        Vector3 origin = shootOrigin.position;
        Vector3 direction = (storedPlayerPosition - origin).normalized;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, attackRange))
        {
            // Draw trail to hit point
            bulletTrails.CreateTrail(origin, hit.point);

            // Optional: damage player if hit
            if (hit.collider.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage();
            }
        }
        else
        {
            // If nothing hit, just draw to max range
            Vector3 endPoint = origin + direction * attackRange;
            bulletTrails.CreateTrail(origin, endPoint);
        }

        Debug.DrawRay(origin, direction * attackRange, Color.yellow, 1f);

        state = EnemyState.Reloading;
        reloadTimer = reloadTime;
    }

    private void UpdateReloading()
    {
        HandleMovement();

        reloadTimer -= Time.deltaTime;

        if (reloadTimer <= 0f)
        {
            state = EnemyState.Idle;
        }
    }

    protected override void Die()
    {
        Debug.Log("Enemy Destroyed!");
        base.Die();
        Destroy(gameObject);
    }
}