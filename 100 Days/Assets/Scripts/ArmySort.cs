using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ArmySort : MonoBehaviour 
{
    public Sprite arrowAsc, arrowDesc;
    public Image levelImg, nameImg, classImg, healthImg;
    
    private bool levelDesc, nameDesc, classDesc, healthDesc;
    private UnitManager unitManagerScript;
    private MenuManager menuManagerScript;

    void Start()
    {
        unitManagerScript = GameObject.Find("UnitsData").GetComponent<UnitManager>();
        menuManagerScript = GameObject.Find("MenuCanvas").GetComponent<MenuManager>();

        // Set to level descending by default
        levelDesc = nameDesc = classDesc = healthDesc = false;
        SortLevel();               
    }

    public void SortLevel()
    {
        setOthersTransparent(levelImg);

        if(levelDesc)
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return a.level.CompareTo(b.level); });
            levelImg.sprite = arrowAsc;
        }
        else
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return b.level.CompareTo(a.level); });
            levelImg.sprite = arrowDesc;
        }

        levelDesc = !levelDesc;
        menuManagerScript.clearRenderedData();
        menuManagerScript.renderData("Army Panel");
        print("Sorted by level");
    }

    public void SortName()
    {
        setOthersTransparent(nameImg);

        if (nameDesc)
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return a.firstName.CompareTo(b.firstName); });
            nameImg.sprite = arrowAsc;
        }
        else
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return b.firstName.CompareTo(a.firstName); });
            nameImg.sprite = arrowDesc;
        }

        nameDesc = !nameDesc;
        menuManagerScript.clearRenderedData();
        menuManagerScript.renderData("Army Panel");
        print("Sorted by name");
    }

    public void SortClass()
    {
        setOthersTransparent(classImg);

        if (classDesc)
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return a.classToString().CompareTo(b.classToString()); });
            classImg.sprite = arrowAsc;
        }
        else
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return b.classToString().CompareTo(a.classToString()); });
            classImg.sprite = arrowDesc;
        }

        classDesc = !classDesc;
        menuManagerScript.clearRenderedData();
        menuManagerScript.renderData("Army Panel");
        print("Sorted by class");
    }

    public void SortHealth()
    {
        setOthersTransparent(healthImg);

        if (healthDesc)
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return a.currentHealth.CompareTo(b.currentHealth); });
            healthImg.sprite = arrowAsc;
        }
        else
        {
            unitManagerScript.allPlayerUnits.Sort(delegate(UnitClass a, UnitClass b)
            { return b.currentHealth.CompareTo(a.currentHealth); });
            healthImg.sprite = arrowDesc;
        }

        healthDesc = !healthDesc;
        menuManagerScript.clearRenderedData();
        menuManagerScript.renderData("Army Panel");
        print("Sorted by remaining health");
    }

    // Set other sort buttons images to transparent
    void setOthersTransparent(Image image)
    {
        List<Image> images = new List<Image>() { levelImg, nameImg, classImg, healthImg };
        images.Remove(image);

        foreach (Image img in images)
        {
            Color color = img.color;
            color.a = 0;
            img.color = color;
        }

        Color arrowColor = image.color;
        arrowColor.a = 1;
        image.color = arrowColor;
    }
}
