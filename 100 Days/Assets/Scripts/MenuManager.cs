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
    private bool renderedArmyData;

    void Start()
    {
        parentPanel = GameObject.Find("Panel Units").GetComponent<RectTransform>();
        renderedArmyData = false;
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

    void renderData(string name)
    {
        if(name == "Army Panel" && !renderedArmyData)
        {
            print("Render Army panel data!!");
            allPlayerUnits = GameObject.Find("UnitsData").GetComponent<UnitManager>().allPlayerUnits;

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
            }
            renderedArmyData = true;
        }
    }
}
