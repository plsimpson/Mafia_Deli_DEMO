using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu: MonoBehaviour
{
    public void OnPlayButton ()
    {
        PlayerInventory.Money = 0;
        PlayerInventory.OwnedItem = null;
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton ()
    {
        Application.Quit();
    }



}