using UnityEngine;

public class Enemy : BaseCharacter
{
    public Transform playerTransform;

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.LookAt(playerTransform);

            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTransform.position,
                speed * Time.deltaTime
            );
        }
    }

    protected override void Die() // Overrides the base Die method
    {
        Debug.Log("Enemy Destroyed!");
        base.Die(); // Calls the base character's Die method as well
        Destroy(gameObject);
    }
}