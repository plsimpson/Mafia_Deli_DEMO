using UnityEngine;

public class ShotgunEquipment : Equipment
{
    public override void Use()
    {
        Debug.Log("BOOM!");
        for (int i = 0; i < 10; i++)
        {
            Vector3 target = transform.position + transform.forward * 8f + Random.insideUnitSphere * Random.Range(-2, 2);
            Debug.DrawLine(transform.position, target, Color.red, 5f);
            if (Physics.Raycast(transform.position, (target - transform.position).normalized, out RaycastHit hit))
            {
                Debug.Log("Shot " + i + " hit " + hit.collider.name);
            }
        }
    }
}
