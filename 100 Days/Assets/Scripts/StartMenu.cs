using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour 
{
    public static bool startNew;

    void Start()
    {
        // Check if savedata exists
        if(PlayerPrefs.GetInt("saves") == 0)
        {
            GameObject.Find("btn_continue").GetComponent<Button>().interactable = false;
        }
    }

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
