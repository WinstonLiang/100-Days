using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour {

    public Slider[] hpSlider; // To update the sprites of the hp bars
    public Slider[] abilitySlider; // To update the sprites of the ability bars

    public GameObject[] playerSlots; // Temporarily set to public for testing
    public GameObject[] enemySlots; // Temporarily set to public for testing 
    public GameObject[] charUIs; // To disable the UI for nonexistent units

    private Battle battleScript; // To grab the information of each unit to update UI (HP/Ability/etc)
    int playerUnitCount, enemyUnitCount; // Used to display bars only for existing units
    int maximumSlots = 4; // Set maximum slots possible (for possible future changes)

    AssaultClass assaultScript;
    DefenderClass defenderScript;
    MedicClass medicScript;

    // Use this for initialization
    void Start ()
    {
        battleScript = GetComponent<Battle>();
        playerUnitCount = battleScript.playerUnits.Count;
        enemyUnitCount = battleScript.enemyUnits.Count;
        hpSlider = new Slider[maximumSlots];
        abilitySlider = new Slider[maximumSlots];
        playerSlots =  new GameObject[maximumSlots];        
        enemySlots =  new GameObject[maximumSlots];
        charUIs = new GameObject[maximumSlots];  

        initBars();
        initializeBattleUI();
        setSlots();
        getClassSprites();
        setSprites();
        disableUnusedSlots();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    // Disables HP bars and slots that aren't used
    void disableUnusedSlots()
    {
        for (int i = 0; i < maximumSlots; i++)
        {
            // Disable player slots and HP/Ability Bars
            if (playerUnitCount < i + 1)
            {
                playerSlots[i].SetActive(false);
                charUIs[i].SetActive(false);
            }

            // Disable enemy slots
            if (enemyUnitCount < i + 1)
            {
                enemySlots[i].SetActive(false);
            }
        }
    }

    // Initialize the HP/Ability bars
    void initBars()
    {
        // For the player units
        for (int i = 0; i < maximumSlots; i++)
        {
            hpSlider[i] = GameObject.Find("hpSlider" + (i + 1)).GetComponent<Slider>();
            abilitySlider[i] = GameObject.Find("abilitySlider" + (i + 1)).GetComponent<Slider>();
        }
    }

    // Initialize the sprites to hpImage
    void initializeBattleUI()
    {
        for (int i = 0; i < playerUnitCount; i++)
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
        assaultScript = GameObject.Find("AssaultGO").GetComponent<AssaultClass>();
        defenderScript = GameObject.Find("DefenderGO").GetComponent<DefenderClass>();
        medicScript = GameObject.Find("MedicGO").GetComponent<MedicClass>();
    }

    // Set the reference of the Player annd Enemy GameObjects 
    void setSlots()
    {
        for (int i = 0; i < maximumSlots; i++)
        {
            playerSlots[i] = GameObject.Find("P" + (i + 1));
            enemySlots[i] = GameObject.Find("E" + (i + 1));
            charUIs[i] = GameObject.Find("CharUI" + (i + 1));

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
        for (int i = 0; i < playerUnitCount; i++)
        {
            print("Setting sprite");
            changeSprite(playerSlots[i].GetComponent<Image>(), battleScript.playerUnits[i].classType);
        }

        for (int i = 0; i < enemyUnitCount; i++)
        {
            print("Setting sprite");
            changeSprite(enemySlots[i].GetComponent<Image>(), battleScript.enemyUnits[i].classType);
        }
    }

    // Change the colors depending on amount
    void setBarColors()
    {
        float currentHP, maxHP, currentPercentage;

        for (int i = 0; i < playerUnitCount; i++)
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
        for (int i = 0; i < playerUnitCount; i++)
        {
            if(battleScript.playerUnits[i].deadFlag)
            {
                playerSlots[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
            }
        }

        for (int i = 0; i < enemyUnitCount; i++)
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
        for (int i = 0; i < playerUnitCount; i++)
        {
            hpSlider[i].value = battleScript.playerUnits[i].currentHealth;
            abilitySlider[i].value = battleScript.playerUnits[i].currentPower;
        }
        setBarColors();
        setDeadColors();
        print("BattleUI updated..");
    }
}
