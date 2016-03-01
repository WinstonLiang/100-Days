using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour 
{
    public static bool startNew;

    public void newGame()
    {
        startNew = true;
        SceneManager.LoadScene("MapNode");
    }

    public void loadGame()
    {
        startNew = false;
        SceneManager.LoadScene("MapNode");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
