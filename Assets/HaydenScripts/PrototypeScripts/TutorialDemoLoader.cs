using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDemoLoader : DemoSceneLoader
{
    [SerializeField] private Tutorial tutorial;
    //Reference the OrderController and check if all orders are complete, if so load the next scene

    private void Update()
    {
        if (tutorial.SandwichOptions.Count <= 0)
        {
            SceneLoader();
        }
    }
}
