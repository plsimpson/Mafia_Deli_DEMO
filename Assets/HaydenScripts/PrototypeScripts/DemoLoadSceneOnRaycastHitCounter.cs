using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLoadSceneOnRaycastHitCounter : DemoSceneLoader
{
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Counter"))
                {
                    SceneLoader();
                }
            }
        }
    }
}
