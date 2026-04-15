using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    protected void SceneLoader()
    {
        SceneManager.LoadScene(sceneName);
    }
}
