using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public GameObject testvar;
    public Menu currentMenu;
    public GameObject panelUnit;
    public GameObject dragUnit;
    public Sprite transparentSprite;

    List<UnitClass> allPlayerUnits;
    Transform parentPanel;
    Menu unitPanel;
    UnitPanelRender unitRenderScript;

    Transform battlePanel; // To use with Squad panel
    Transform parentDragPanel;
    DragScript dragScript;
    UnitManager unitManager;    

    Transform renamePanel; // To use with renaming
    UnitClass currentUnit;

    Transform skillPanel; // To use with R&D
    RDManager rDManager;
    bool rDLoaded;

    AssaultClass assaultScript;
    DefenderClass defenderScript;
    MedicClass medicScript;

    void Start()
    {
        parentPanel = GameObject.Find("Panel Units").GetComponent<RectTransform>();
        unitPanel = GameObject.Find("Unit Panel").GetComponent<Menu>();
        unitRenderScript = GameObject.Find("Unit Panel").GetComponent<UnitPanelRender>();
        battlePanel = GameObject.Find("Battle Panel").transform;
        dragScript = battlePanel.GetComponent<DragScript>();
        parentDragPanel = GameObject.Find("Panel DragUnits").transform;
        unitManager = GameObject.Find("UnitsData").GetComponent<UnitManager>();
        renamePanel = GameObject.Find("Rename Panel").transform;
        CloseRenamePanel();
        skillPanel = GameObject.Find("Skill Panels").transform;
        assaultScript = GameObject.Find("AssaultGO").GetComponent<AssaultClass>();
        defenderScript = GameObject.Find("DefenderGO").GetComponent<DefenderClass>();
        medicScript = GameObject.Find("MedicGO").GetComponent<MedicClass>();

        //Set up R&D buttons/text when scene is loaded
        rDManager = GameObject.Find("UnitsData").GetComponent<RDManager>();
        renderData("R&D Panel");
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
        else if (classType == -1)
        {
            image.sprite = transparentSprite;
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

    // Overload the ShowMenu function to use for unit panel
    public void ShowMenu(Menu menu, int unitIndex)
    {
        if (currentMenu != null)
            currentMenu.IsOpen = false;

        unitRenderScript.renderUnitData(allPlayerUnits[unitIndex]);
        currentUnit = allPlayerUnits[unitIndex];

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

    //Attached to rename button
    public void RenamePanel()
    {
        renamePanel.gameObject.SetActive(true);
    }

    //Submit the new unit name
    public void SubmitRename(InputField newName)
    {
        if (newName.text.Trim() != "")
        {
            currentUnit.lastName = "";
            currentUnit.firstName = newName.text;
            unitRenderScript.renderUnitData(currentUnit);
            newName.text = "";
        }
        CloseRenamePanel();
    }

    //Close the rename panel
    public void CloseRenamePanel()
    {
        renamePanel.gameObject.SetActive(false);
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
        if (UnitManager.DEBUG) print("Destroyed previous rendered data");
    }

    public void clearRenderedDragData()
    {
        for (int i = 0; i < parentDragPanel.childCount; i++)
        {
            Destroy(parentDragPanel.GetChild(i).gameObject);
        }
        if (UnitManager.DEBUG) print("Destroyed previous rendered drag panel data");
    }

    public void renderData(string name)
    {
        if (name == "Army Panel")
        {
            #region Army Panel
            if (UnitManager.DEBUG) print("Render Army panel data!!");
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
            #endregion
        }
        else if (name == "Squad Panel")
        {
            #region Squad Panel
            //Initialize the battle members
            List<UnitClass> playerUnits = unitManager.getBattlingSquad();
            int battlePanelCount = battlePanel.childCount;
            for (int i = 0; i < battlePanelCount; i++)
            {
                //Check against max size of playerUnits
                if (playerUnits.Count > i)
                {
                    testvar = battlePanel.GetChild(i).GetChild(1).gameObject;
                    changeSprite(battlePanel.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>(), playerUnits[i].classType);
                    battlePanel.GetChild(i).GetChild(0).GetComponent<Text>().text = playerUnits[i].firstName + " " + playerUnits[i].lastName;
                }
                else
                {
                    changeSprite(battlePanel.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>(), -1);
                    battlePanel.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
                }
            }

            //Initialize the reserves list
            clearRenderedDragData();
            foreach (UnitClass unit in allPlayerUnits)
            {
                if (unit.squad == 1) // 1 = reserve squad
                {
                    GameObject unitClone = Instantiate(dragUnit);
                    unitClone.transform.SetParent(parentDragPanel, false);

                    #region Add event triggers
                    EventTrigger trigger = unitClone.GetComponent<EventTrigger>();
                    
                    //Drag
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.Drag;
                    entry.callback = new EventTrigger.TriggerEvent();
                    entry.callback.AddListener((eventData) => { dragScript.OnDragBattleMember(eventData); });
                    trigger.triggers.Add(entry);

                    //Pointer click
                    EventTrigger.Entry entry2 = new EventTrigger.Entry();
                    entry2.eventID = EventTriggerType.PointerDown;
                    entry2.callback = new EventTrigger.TriggerEvent();
                    entry2.callback.AddListener((eventData) => { dragScript.OnMemberClicked(eventData); });
                    trigger.triggers.Add(entry2);

                    //End drag
                    EventTrigger.Entry entry3 = new EventTrigger.Entry();
                    entry3.eventID = EventTriggerType.EndDrag;
                    entry3.callback = new EventTrigger.TriggerEvent();
                    entry3.callback.AddListener((eventData) => { dragScript.OnDragEndBattleMember(eventData); });
                    trigger.triggers.Add(entry3);
                    #endregion

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
                }
            }
            #endregion
        }
        else if (name == "R&D Panel")
        {
            #region R&D Panel
            if (!rDLoaded)
            {
                //Iterate through each panel under Skill Panels
                rDManager.checkMaxLevels();
                int skillPanels = skillPanel.childCount;
                for (int i = 0; i < skillPanels; i++)
                {
                    SkillTree currentSkilltree = rDManager.skillTrees[i];
                    rDManager.SetLockedSkill(currentSkilltree.ability1Lock, skillPanel.GetChild(i).GetChild(2));
                    rDManager.SetLockedSkill(currentSkilltree.ability2Lock, skillPanel.GetChild(i).GetChild(3));
                    rDManager.SetLockedSkill(currentSkilltree.ability3Lock, skillPanel.GetChild(i).GetChild(4));
                }

                rDLoaded = true;
            }
            #endregion
        }
    }
}
