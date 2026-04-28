using UnityEngine;
using UnityEngine.SceneManagement;
public class EndMenu: MonoBehaviour
{
    public void OnPlayButton ()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton ()
    {
        Application.Quit();
    }



}