using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("LeonTest");
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

