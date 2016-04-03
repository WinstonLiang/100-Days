using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Menu currentMenu;
    public GameObject panelUnit;

    List<UnitClass> allPlayerUnits;
    Transform parentPanel;
    Menu unitPanel;
    UnitPanelRender unitRenderScript;

    AssaultClass assaultScript;
    DefenderClass defenderScript;
    MedicClass medicScript;

    void Start()
    {
        parentPanel = GameObject.Find("Panel Units").GetComponent<RectTransform>();
        unitPanel = GameObject.Find("Unit Panel").GetComponent<Menu>();
        unitRenderScript = GameObject.Find("Unit Panel").GetComponent<UnitPanelRender>();
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
        print("asdflkjs");
        renderData(menu.name);

        currentMenu = menu;
        currentMenu.IsOpen = true;
    }

    // Overload the ShowMenu function to use for unit panel
    public void ShowMenu(Menu menu, int unitIndex)
    {
        if (currentMenu != null)
            currentMenu.IsOpen = false;

        unitRenderScript.renderUnitData(allPlayerUnits[unitIndex]);

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

    public void OpenOptions(Menu menu)
    {
        menu.IsOpen = !menu.IsOpen;
    }

    public void CloseOptions(Menu menu)
    {
        if (menu.IsOpen)
            menu.IsOpen = false;
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
        if (name == "Army Panel")
        {
            print("Render Army panel data!!");
            allPlayerUnits = GameObject.Find("UnitsData").GetComponent<UnitManager>().allPlayerUnits;
            clearRenderedData();
            int unitIndex = 0;

            foreach (UnitClass unit in allPlayerUnits)
            {
                GameObject unitClone = Instantiate(panelUnit);
                unitClone.transform.SetParent(parentPanel, false);

                // Set up an array to be used with setting the Text objects
                string[] unitInfo = { unit.level.ToString(), unit.firstName, unit.classToString(), 
                    unit.currentHealth.ToString() + "/" + unit.maxHealth.ToString() };

                // Traverse through it's child Text objects and set the data accordingly
                // textParent is the button for the unit
                Transform textParent = unitClone.transform.GetChild(0);
                int textObjects = textParent.childCount;
                for (int i = 0; i < textObjects; i++)
                {
                    textParent.GetChild(i).GetComponent<Text>().text = unitInfo[i];
                }

                // Set the correct image for the sprite
                changeSprite(textParent.GetChild(0).GetChild(0).GetComponent<Image>(), unit.classType);

                // Set the listener event for the button to transition to unit screen              
                int _unitIndex = unitIndex++;
                textParent.GetComponent<Button>().onClick.AddListener(() => ShowMenu(unitPanel, _unitIndex));                
            }
        }
        else if (name == "Squad Panel")
        {
            // How to arrange squad menu / allocate units to squads?
            print("Showing Squad panel");
        }
        else if (name == "R&D Panel")
        {

        }
    }
}
