using UnityEngine;
using UnityEngine.SceneManagement;
public class EndMenu: MonoBehaviour
{
    public void OnPlayButton ()
    {
        PlayerInventory.Money = 0;
        PlayerInventory.OwnedItem = null;
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton ()
    {
        Application.Quit();
    }



}