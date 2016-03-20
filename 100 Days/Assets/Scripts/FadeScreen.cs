using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public float initialAlpha, fadeSpeed;
    public Color initialColor;
    public bool faded;

    Image image;
    GameObject loadingText;
    bool fadingIn, fadingOut;

    void Start()
    {
        faded = true;
        image = GetComponent<Image>();
        loadingText = transform.GetChild(0).gameObject;
        loadingText.SetActive(false);

        // Initialize fadeSpeed if not set or <= 0
        if (fadeSpeed <= 0 || fadeSpeed == null)
            fadeSpeed = 1;
    }

    void setInitialAlpha()
    {
        Color color = initialColor;
        color.a = initialAlpha;
        image.color = color;
    }

    void FixedUpdate()
    {
        Color color = image.color;

        if(fadingOut)
        {
            if (color.a < 1.0f)
            {
                color.a += 0.1f * fadeSpeed * Time.fixedDeltaTime;
                image.color = color;
            }
            else
            {
                fadingOut = false;
                faded = true;
            }                
        }
        else if(fadingIn)
        {
            if (color.a > 0.0f)
            {
                color.a -= 0.1f * fadeSpeed * Time.fixedDeltaTime;
                image.color = color;
            }
            else
            {
                fadingIn = false;
                faded = true;
            }                
        }
    }

    public void initializeScreen(Transform canvasParent)
    {
        transform.SetParent(canvasParent);
        setInitialAlpha();
        image.enabled = false;
    }

    public void FadeOut()
    {
        fadingOut = image.enabled = true;
        faded = false;
    }

    public void FadeIn()
    {
        fadingIn = image.enabled = true;
        faded = false;
    }

    public void LoadingText()
    {
        loadingText.SetActive(true);
    }
}
