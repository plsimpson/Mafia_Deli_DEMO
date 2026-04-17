using UnityEngine;

public class ChairLauncher : MonoBehaviour
{
    [SerializeField] private Transform camTrans;
    [SerializeField] private float launchForce = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camTrans.position, camTrans.forward, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Chair"))
                {
                    Debug.Log("Hit a chair!");
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.linearVelocity = camTrans.forward * launchForce;
                    }
                }
            }
        }
    }
}