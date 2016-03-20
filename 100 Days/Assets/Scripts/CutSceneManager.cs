using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour {

    public static int cutSceneId = 0;
    public int[] introDisplayOrder;

    int[] displayOrder;
    FadeScreen fadeScreen;
    Transform images, texts;
    int currentPage, displayCode;
    bool changeScene;

	void Start () 
    {
        currentPage = displayCode = 0;
        changeScene = false;
        setDisplayOrder();
        fadeScreen = GameObject.Find("FadeScreen").GetComponent<FadeScreen>();
        fadeScreen.initializeScreen(transform);
        fadeScreen.FadeIn();
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0) && displayOrder != null && displayOrder.Length > 0)
        {
            displayNext();
        }
	    
        if (fadeScreen.faded && changeScene)
        {
            fadeScreen.LoadingText();
            SceneManager.LoadScene("MapNode");
        }
	}

    // Assign cutscene IDs here
    void setDisplayOrder()
    {
        Transform cutSceneHolder = transform.GetChild(cutSceneId);

        if (cutSceneId == 0)
        {
            // First child of canvas is the intro cutscene
            displayOrder = introDisplayOrder;            
        }

        // Get the images and texts containers
        images = cutSceneHolder.GetChild(0);
        texts = cutSceneHolder.GetChild(1);

        // Display initial image
        displayNext();
    }

    // Load the image, text, or both depending on page
    void displayNext()
    {
        if (currentPage < displayOrder.Length)
        {
            displayCode = displayOrder[currentPage];

            if (images.childCount > 0 || texts.childCount > 0)
            {
                if (displayCode == 0)
                {
                    if (currentPage != 0)
                        Destroy(images.GetChild(0).gameObject);
                    // 0 denotes display next image
                    images.GetChild(0).gameObject.SetActive(true);
                }
                else if (displayCode == 1)
                {
                    // 1 denotes display next text
                    Destroy(texts.GetChild(0).gameObject);
                    texts.GetChild(1).gameObject.SetActive(true);
                }
                else if (displayCode == 2)
                {
                    // 2 denotes display next image and text
                    Destroy(images.GetChild(0).gameObject);
                    Destroy(texts.GetChild(0).gameObject);
                    images.GetChild(0).gameObject.SetActive(true);
                    texts.GetChild(0).gameObject.SetActive(true);
                }
                currentPage++;
            }
        }            
        else
        {
            // Finished showing cutscenes
            changeScene = true;
            fadeScreen.FadeOut();
        }      
    }
}
