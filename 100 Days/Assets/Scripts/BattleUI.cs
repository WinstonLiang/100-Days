using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUI : MonoBehaviour {

    public Battle battleScript; // To grab the information of each unit to update UI (HP/Ability/etc)
    public GameObject[] HPBarsObject = new GameObject[4]; // To update the sprites of the hp bars
    public Image[] HPBars = new Image[4];
    public GameObject[] abilityBarsObject = new GameObject[4]; // To update the sprites of the ability bars
    public Image[] abilityBars = new Image[4];
    public Sprite hpImage;


    // Use this for initialization
    void Start ()
    {
        battleScript = GetComponent<Battle>();
        initBars();
        initializeBattleUI();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    // Initialize the HP/Ability bars
    void initBars()
    {
        for (int i = 0; i < 4; i++)
        {
            HPBarsObject[i] = GameObject.Find("HP" + (i + 1));
            HPBars[i] = HPBarsObject[i].GetComponent<Image>();
            abilityBarsObject[i] = GameObject.Find("Ability" + (i + 1));
            abilityBars[i] = abilityBarsObject[i].GetComponent<Image>();
        }
    }

    void initializeBattleUI()
    {
        for (int i = 0; i < battleScript.playerUnits.Count; i++)
        {
            HPBars[i].sprite = hpImage;
            abilityBars[i].sprite = hpImage;
        }
        print("BattleUI initialized.");
    }

    // Update battle UI
    public void updateBattleUI()
    {
        for(int i = 0; i < battleScript.playerUnits.Count; i++)
        {
            float currentPercentage = (float)battleScript.playerUnits[i].currentHealth / (float)battleScript.playerUnits[i].maxHealth;
            float hpWidth = HPBarsObject[i].GetComponent<RectTransform>().rect.width * currentPercentage;
            print("Current: " + currentPercentage + "%");
            HPBarsObject[i].GetComponent<RectTransform>().sizeDelta = new Vector2(hpWidth, 16.1f);
            print(HPBars[i].sprite.bounds.min.x + " : " + HPBars[i].sprite.bounds.max.x);
            
        }
        print("BattleUI updated..");
    }
}
