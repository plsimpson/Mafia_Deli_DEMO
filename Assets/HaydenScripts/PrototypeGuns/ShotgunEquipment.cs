using Game;
using UnityEngine;

public class ShotgunEquipment : Equipment
{
    public float reloadTime = 2f;

    private void Update()
    {
        reloadTime -= Time.deltaTime;
    }

    public override void Use()
    {
        if (reloadTime <= 0)
        {
            Shoot();
            reloadTime = 2f;
        }
    }

    private void Shoot()
    {
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, playerCamTrans.position);
        }
        Debug.Log("BOOM!");
        for (int i = 0; i < 5; i++)
        {
            Vector3 target = playerCamTrans.position + playerCamTrans.forward * 8f + Random.insideUnitSphere * Random.Range(-2, 2);
            Debug.DrawLine(playerCamTrans.position, target, Color.red, 5f);
            if (Physics.Raycast(playerCamTrans.position, (target - playerCamTrans.position).normalized, out RaycastHit hit))
            {
                Debug.Log("Shot " + i + " hit " + hit.collider.name);

                bulletTrails.CreateTrail(bulletTrails.bulletTrailOrigin.position, hit.point);

                if (hit.collider.TryGetComponent(out MeleeEnemyNavigationStateMachine enemy))
                {
                    enemy.TakeDamage(5);
                }

                if (hit.collider.TryGetComponent(out MovingEnemy movingenemy))
                {
                    movingenemy.TakeDamage(5);
                }
            }

        }
    }
}
