using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class UnitClass
{
    public GameObject unitName; //Get the unit name
    public GameObject unit; //The type of unit    
    public string firstName, lastName;
    public int level = 10;
    public int currentHealth, maxHealth;
    public int att, def;
    public int currentSpeed, maxSpeed;
    public int currentPower, maxPower;
    public int crit, dodge;
    public int substat;
    public bool deadFlag;
    public int classType;
    public int squad;
    public int ablity;

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

        firstName = "default firstname";
        lastName = "default lastname";
        //nameSelector.getName();
        //firstName = nameSelector.firstName;
        //lastName = nameSelector.lastName;
    }

    // Change the class of the unit
    public void classChange(int otherClass)
    {
        if (otherClass == 1)
        {
            AssaultClass type = unit.GetComponent<AssaultClass>();
        }
        else if (otherClass == 2)
        {
            DefenderClass type = unit.GetComponent<DefenderClass>();
        }
        else if (otherClass == 3)
        {
            MedicClass type = unit.GetComponent<MedicClass>();
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
            allPlayerUnits.Add(newUnit);
            print("Added Player: " + allPlayerUnits[add].firstName + " " + allPlayerUnits[add].lastName);                    
        }        

        // Same applies with enemies for testing purposes
        for (int add = 0; add < allEnemyUnits.Capacity; add++)
        {
            UnitClass newUnit = new UnitClass();
            nameSelector.getName();
            newUnit.changeName(nameSelector.firstName, nameSelector.lastName);
            allEnemyUnits.Add(newUnit);
            print("Added Enemy: " + allEnemyUnits[add].firstName + " " + allEnemyUnits[add].lastName);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        //print("update: " + allPlayerUnits.Count);
    }

    ///Return the squad in battle
    public List<UnitClass> getBattlingSquad()
    {
        List<UnitClass> unitsInBattle = new List<UnitClass>(maxPlayers);

        //print(allPlayerUnits.Count);

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

