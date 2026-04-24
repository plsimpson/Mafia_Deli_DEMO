using Game;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    [SerializeField] private Transform playerCamTrans;

    [SerializeField] private BulletTrails bulletTrails;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
}
