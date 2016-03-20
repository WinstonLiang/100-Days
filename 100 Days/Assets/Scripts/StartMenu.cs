using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour 
{
    public static bool startNew;
    
    FadeScreen fadeScreen;
    bool changeScene;           // Used to manage fading in/out timing

    void Start()
    {
        // Check if savedata exists
        if(PlayerPrefs.GetInt("saves") == 0)
        {
            GameObject.Find("btn_continue").GetComponent<Button>().interactable = false;
        }

        // Make sure the fade screen is turned off and alpha is set properly
        fadeScreen = GameObject.Find("FadeScreen").GetComponent<FadeScreen>();
        fadeScreen.initializeScreen(transform);
        changeScene = false;        
    }

    void Update()
    {
        if (fadeScreen.faded && changeScene)
        {
            if (startNew)
            {
                changeScene = false;
                fadeScreen.LoadingText();
                CutSceneManager.cutSceneId = 0;
                SceneManager.LoadScene("CutScenes");
            }
            else
            {
                changeScene = false;
                fadeScreen.LoadingText();
                SceneManager.LoadScene("MapNode");
            }
        }
    }

    public void newGame()
    {
        startNew = changeScene = true;
        fadeScreen.FadeOut();        
    }

    public void loadGame()
    {
        startNew = false;
        changeScene = true;
        fadeScreen.FadeOut();  
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
