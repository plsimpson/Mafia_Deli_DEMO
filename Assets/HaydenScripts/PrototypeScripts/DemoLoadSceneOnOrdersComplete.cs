using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLoadSceneOnOrdersComplete : DemoSceneLoader
{
    [SerializeField] private OrderController OrderController;
    //Reference the OrderController and check if all orders are complete, if so load the next scene

    private void Update()
    {
        if (OrderController.SandwichOptions.Count <= 0)
        {
            SceneLoader();
        }
    }
}
