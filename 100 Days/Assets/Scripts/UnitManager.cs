using UnityEngine;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;

[System.Serializable]
public class UnitClass
{
    public string firstName, lastName;
    public int level = 10;
    public int currentHealth, maxHealth;
    public int att, def;
    public int currentSpeed, maxSpeed;
    public int currentPower, maxPower;
    public int crit, dodge;
    public int substat;
    public bool deadFlag, healing;
    public int classType;
    public int squad;
    public int ablity;
    public int healingCounter;

    private int gender;

    // Constructor
    public UnitClass()
    {
        level = 1;
        currentHealth = 100;
        maxHealth = 100;
        att = 30; // temporarily set from 5 to 30 for testing
        def = 5;
        currentSpeed = 10;
        maxSpeed = 10;
        currentPower = 50;
        maxPower = 50;
        crit = 1;
        dodge = 1;
        substat = 0;
        deadFlag = false;
        classType = 0;
        squad = 0;
        ablity = 0;
        healing = false;


        firstName = "default firstname";
        lastName = "default lastname";        
    }

    // Change the class of the unit
    public void classChange(UnitClass unit)
    {
        getClassScript(unit.classType).classChange(unit) ;
    }

    // Retrieves the reference to it's own class script
    public Classes getClassScript(int classType)
    {
        if (classType == 1)
        {
            return GameObject.Find("BattleObject").GetComponent<AssaultClass>();
        }
        else if (classType == 2)
        {
            return GameObject.Find("BattleObject").GetComponent<DefenderClass>();
        }
        else // Add more if statements for more classes (classType == 3)
        {
            return GameObject.Find("BattleObject").GetComponent<MedicClass>();
        }
    }

    // Change the stats of the unit
    public void changeStat(int statChange, int orginalStat)
    {
        orginalStat = orginalStat - statChange;
    }

    // Change the name of the unit
    public void changeName(string first, string last)
    {
        firstName = first;
        lastName = last;
    }
}

public class UnitManager : MonoBehaviour
{
    public NameSelector nameSelector;
    public List<UnitClass> allPlayerUnits = new List<UnitClass>(4);
    public List<UnitClass> allEnemyUnits = new List<UnitClass>(4); 
    public int maxPlayers = 4;    
    public int battlingSquad = 0;

    private int enemySet = 0; // this variable should be retrieved from the Map/Game Manager elsewhere

    // Use this for initialization
    void Start()
    {     
        DontDestroyOnLoad(transform.gameObject);
        nameSelector = GetComponent<NameSelector>();
        allPlayerUnits.Capacity = maxPlayers;
        allEnemyUnits.Capacity = maxPlayers;

        // For testing, Simulates autopopulating the player units with
        // starting amount of units when game is started
        for (int add = 0; add < allPlayerUnits.Capacity; add++)
        {            
            UnitClass newUnit = new UnitClass();
            nameSelector.getName();            
            newUnit.changeName(nameSelector.firstName, nameSelector.lastName);

            // Temporarily change class to test sprites
            newUnit.classType = add;
            newUnit.classChange(newUnit);            

            allPlayerUnits.Add(newUnit);
            print("Added Player: " + allPlayerUnits[add].firstName + " " + allPlayerUnits[add].lastName);                    
        }        

        // Same applies with enemies for testing purposes
        for (int add = 0; add < allEnemyUnits.Capacity; add++)
        {
            UnitClass newUnit = new UnitClass();
            nameSelector.getName();
            newUnit.changeName(nameSelector.firstName, nameSelector.lastName);

            // Temporarily change class to test sprites
            newUnit.classType = add;
            newUnit.classChange(newUnit);            

            allEnemyUnits.Add(newUnit);
            print("Added Enemy: " + allEnemyUnits[add].firstName + " " + allEnemyUnits[add].lastName);
        }

        // Switch the level to test dont destroy on load
        print("Loading level now");
        Application.LoadLevel("LeonTest");
        // Replace with SceneManager.LoadLevel("LeonTest"); after updating to 5.3
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///Return the squad in battle
    public List<UnitClass> getBattlingSquad()
    {
        List<UnitClass> unitsInBattle = new List<UnitClass>(maxPlayers); 

        foreach (UnitClass unit in allPlayerUnits)
        {            
            if (unit.squad == battlingSquad)
            {
                unitsInBattle.Add(unit);                
            }
        }
        return unitsInBattle;
    }

    // Return the proper enemy set based on map coordinates - temporarily set
    public List<UnitClass> getEnemySquad()
    {
        List<UnitClass> unitsInBattle = new List<UnitClass>(maxPlayers);       

        foreach (UnitClass unit in allEnemyUnits)
        {
            if (unit.squad == enemySet)
            {
                unitsInBattle.Add(unit);
            }
        }
        return unitsInBattle;
    }
}

