using UnityEngine;

public class TommyEquipment : Equipment
{
    public float fireRate = 0.1f;
    private float nextFireTime;

    public override void Use()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, playerCamTrans.position);
        }

        if (Physics.Raycast(playerCamTrans.position, playerCamTrans.forward, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.name);

            bulletTrails.CreateTrail(
                bulletTrails.bulletTrailOrigin.position,
                hit.point
            );

            if (hit.collider.TryGetComponent(out MeleeEnemyNavigationStateMachine enemy))
            {
                enemy.TakeDamage(10);
            }

            if (hit.collider.TryGetComponent(out MovingEnemy movingenemy))
            {
                movingenemy.TakeDamage(10);
            }
        }
    }
}