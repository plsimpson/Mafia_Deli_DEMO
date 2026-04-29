using UnityEngine;

namespace Game.Weapons
{
    //[SerializeField] private BulletTrails bulletTrails;

    public class Shotgun : MonoBehaviour
    {
        public void Use()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 target = transform.forward + Random.insideUnitSphere * Random.Range(-4, 4);
                Debug.DrawLine(transform.position, target, Color.red, 5f);
                //bulletTrails.CreateTrail(bulletTrails.bulletTrailOrigin.position, hit.point);
                if (Physics.Raycast(transform.position, (target - transform.position).normalized, out RaycastHit hit))
                {
                    Debug.Log("Shot " + i + " hit " + hit.collider.name);
                }
            }
        }
    }
}