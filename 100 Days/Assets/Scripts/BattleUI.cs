using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour {

    public Battle battleScript; // To grab the information of each unit to update UI (HP/Ability/etc)
    public Slider[] hpSlider = new Slider[4]; // To update the sprites of the hp bars
    public Slider[] abilitySlider = new Slider[4]; // To update the sprites of the ability bars

    public GameObject[] playerSlots = new GameObject[4]; // Temporarily set to public for testing
    public GameObject[] enemySlots = new GameObject[4]; // Temporarily set to public for testing 

    GameObject classScriptsGameObject; // A reference to the GameObject containing all the class scripts
    AssaultClass assaultScript;
    DefenderClass defenderScript;
    MedicClass medicScript;

    // Use this for initialization
    void Start ()
    {
        battleScript = GetComponent<Battle>();
        initBars();
        initializeBattleUI();
        setSlots();
        classScriptsGameObject = GameObject.Find("ClassScripts");
        getClassSprites();
        setSprites();     
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    // Initialize the HP/Ability bars
    void initBars()
    {
        for (int i = 0; i < battleScript.playerUnits.Count; i++)
        {
            hpSlider[i] = GameObject.Find("hpSlider" + (i + 1)).GetComponent<Slider>();
            abilitySlider[i] = GameObject.Find("abilitySlider" + (i + 1)).GetComponent<Slider>();
        }
    }

    // Initialize the sprites to hpImage
    void initializeBattleUI()
    {
        for (int i = 0; i < hpSlider.Length; i++)
        {
            hpSlider[i].maxValue = battleScript.playerUnits[i].maxHealth;
            hpSlider[i].value = battleScript.playerUnits[i].currentHealth;
            abilitySlider[i].maxValue = battleScript.playerUnits[i].maxPower;
            abilitySlider[i].value = battleScript.playerUnits[i].currentPower;
        }
        setBarColors();

        print("BattleUI initialized.");
    }

    // Get the sprites for each class
    void getClassSprites()
    {
        // Assign class scripts
        assaultScript = classScriptsGameObject.GetComponent<AssaultClass>();
        defenderScript = classScriptsGameObject.GetComponent<DefenderClass>();
        medicScript = classScriptsGameObject.GetComponent<MedicClass>();
    }

    // Set the reference of the Player annd Enemy GameObjects 
    void setSlots()
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            playerSlots[i] = GameObject.Find("P" + (i + 1));
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            enemySlots[i] = GameObject.Find("E" + (i + 1));
        }
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

    // Sets the player and enemy sprites depending on their classes
    void setSprites()
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            print("Setting sprite");
            changeSprite(playerSlots[i].GetComponent<Image>(), battleScript.playerUnits[i].classType);
            changeSprite(enemySlots[i].GetComponent<Image>(), battleScript.enemyUnits[i].classType);
        }
    }

    // Change the colors depending on amount
    void setBarColors()
    {
        float currentHP, maxHP, currentPercentage;

        for (int i = 0; i < hpSlider.Length; i++)
        {
            currentHP = battleScript.playerUnits[i].currentHealth;
            maxHP = battleScript.playerUnits[i].maxHealth;
            currentPercentage = (float)currentHP / (float)maxHP;

            ColorBlock colorBlock = hpSlider[i].colors;
            colorBlock.disabledColor = new Color(currentHP >= maxHP/2 ? (1-currentPercentage)*2 : 1, 
                                                 currentHP < maxHP ? currentPercentage*2 : 1, 0);
            hpSlider[i].colors = colorBlock;            
        }
    }

    // Change dead player image to dark tint
    void setDeadColors()
    {
        for (int i = 0; i < battleScript.playerUnits.Count; i++)
        {
            if(battleScript.playerUnits[i].deadFlag)
            {
                playerSlots[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            }
        }

        for (int i = 0; i < battleScript.enemyUnits.Count; i++)
        {
            if (battleScript.enemyUnits[i].deadFlag)
            {
                enemySlots[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }

    // Update battle UI
    public void updateBattleUI()
    {
        for (int i = 0; i < hpSlider.Length; i++)
        {
            hpSlider[i].value = battleScript.playerUnits[i].currentHealth;
            abilitySlider[i].value = battleScript.playerUnits[i].currentPower;
        }
        setBarColors();
        setDeadColors();
        print("BattleUI updated..");
    }
}
