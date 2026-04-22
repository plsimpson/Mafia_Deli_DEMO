using UnityEngine;

public class GunDamage : MonoBehaviour
{
    [SerializeField] private Transform playerCamTrans;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamTrans.position, playerCamTrans.forward, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out MeleeEnemyNavigationStateMachine enemy))
                {
                    enemy.TakeDamage(10);
                    //Change the 10 to a variable determined by a weapon class
                }
                if (hit.collider.TryGetComponent(out MovingEnemy movingenemy))
                {
                    movingenemy.TakeDamage(10);
                    //Change the 10 to a variable determined by a weapon class
                }
            }
        }
    }
}
