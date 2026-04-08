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

    void Start()
    {
        minX = transform.position.x;
        maxX = transform.position.x + distanceOffsetX;

        minZ = transform.position.z;
        maxZ = transform.position.z + distanceOffsetZ;
    }

    void Update()
    {
        if (distanceOffsetX > 0f)
        {
            xTransform = Mathf.PingPong(Time.time * speed, maxX - minX) + minX;
        }
        else
        {
            xTransform = transform.position.x; // Keep X constant if no offset
        }

        if (distanceOffsetZ > 0f)
        {
            zTransform = Mathf.PingPong(Time.time * speed, maxZ - minZ) + minZ;
        }
        else
        {
            zTransform = transform.position.z; // Keep Z constant if no offset
        }

        transform.position = new Vector3(
                xTransform,
                transform.position.y,
                zTransform
            );

        if (playerTransform != null)
        {
            transform.LookAt(playerTransform);
        }
    }

    protected override void Die() // Overrides the base Die method
    {
        Debug.Log("Enemy Destroyed!");
        base.Die(); // Calls the base character's Die method as well
        Destroy(gameObject);
    }
}