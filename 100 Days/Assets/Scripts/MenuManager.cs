using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Menu currentMenu;
    public GameObject panelUnit;

    private List<UnitClass> allPlayerUnits;
    private Transform parentPanel;

    private AssaultClass assaultScript;
    private DefenderClass defenderScript;
    private MedicClass medicScript;

    void Start()
    {
        parentPanel = GameObject.Find("Panel Units").GetComponent<RectTransform>();
        assaultScript = GameObject.Find("AssaultGO").GetComponent<AssaultClass>();
        defenderScript = GameObject.Find("DefenderGO").GetComponent<DefenderClass>();
        medicScript = GameObject.Find("MedicGO").GetComponent<MedicClass>();
    }

    // Set the sprite depending on the class type
    void changeSprite(Image image, int classType)
    {
        if (classType == 0)
        {
            // Set Apprentice/Infantry sprite here
            image.sprite = medicScript.idleSprite;
        }
        else if (classType == 1)
        {
            image.sprite = assaultScript.idleSprite;
        }
        else if (classType == 2)
        {
            image.sprite = defenderScript.idleSprite;
        }
        else if (classType == 3)
        {
            image.sprite = medicScript.idleSprite;
        }
    }

    public void ShowMenu(Menu menu)
    {
        if (currentMenu != null)
            currentMenu.IsOpen = false;

        renderData(menu.name);

        currentMenu = menu;
        currentMenu.IsOpen = true;
    }

    public void ShowMap()
    {
        if (currentMenu != null)
        {
            currentMenu.IsOpen = false;
            currentMenu = null;
        }
    }

    public void clearRenderedData()
    {
        for (int i = 0; i < parentPanel.childCount; i++)
        {
            Destroy(parentPanel.GetChild(i).gameObject);
        }
        print("Destroyed previous rendered data");
    }

    public void renderData(string name)
    {
        if(name == "Army Panel")
        {
            print("Render Army panel data!!");
            allPlayerUnits = GameObject.Find("UnitsData").GetComponent<UnitManager>().allPlayerUnits;
            clearRenderedData();

            foreach (UnitClass unit in allPlayerUnits)
            {
                GameObject unitClone = Instantiate(panelUnit);
                unitClone.transform.SetParent(parentPanel, false);

                // Set up an array to be used with setting the Text objects
                string[] unitInfo = { unit.level.ToString(), unit.firstName, unit.classToString(), 
                    unit.currentHealth.ToString() + "/" + unit.maxHealth.ToString() };

                // Traverse through it's child Text objects and set the data accordingly
                Transform textParent = unitClone.transform.GetChild(0);
                int textObjects = textParent.childCount;
                for (int i = 0; i < textObjects; i++)
                {
                    textParent.GetChild(i).GetComponent<Text>().text = unitInfo[i];
                }

                // Set the correct image for the sprite
                changeSprite(textParent.GetChild(0).GetChild(0).GetComponent<Image>(), unit.classType);
            }
        }
    }
}
