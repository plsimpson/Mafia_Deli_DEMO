using Game;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PistolEquipment : Equipment
{
    public float reloadTime = 1f;

    private void Start()
    {
        playerCamTrans = Camera.main.transform;

        bulletTrails = GetComponent<BulletTrails>();
    }

    private void Update()
    {
        reloadTime -= Time.deltaTime;
    }

    public override void Use()
    {
        if (reloadTime <= 0)
        {
            Shoot();
            reloadTime = 1f;
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

            bulletTrails.CreateTrail(bulletTrails.bulletTrailOrigin.position, hit.point);

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
