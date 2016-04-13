using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable] 
public class UnitManager : MonoBehaviour
{     
    public List<UnitClass> allPlayerUnits = new List<UnitClass>(4);
    public List<UnitClass>[][] allEnemyUnits;
    public int initialPlayerUnitCount = 3;
    public int maxPlayers = 4;    // Total number of units of a side that participates in a battle at the same time

    // To be used by Battle script 
    public int battlingSquad = 0;   // 0 is the active battling squad - 1 are the reserves
    public int enemyXCoord = 0;
    public int enemyYCoord = 0;

    MapGenerator mapGenerator;

    NameSelector nameSelector;
    int maxX, maxY;   // Size of the map

    // Hold a reference to this object to keep singleton
    static UnitManager unitManagerRef;

    // Use this for initialization
    void Start()
    {
        // To prevent duplicate unitManagers
        if (unitManagerRef == null)
        {
            unitManagerRef = this;
            DontDestroyOnLoad(gameObject);

            nameSelector = GetComponent<NameSelector>();
            mapGenerator = GetComponent<MapGenerator>();

            // Get map size from the map Script and resize the array
            //temporarily set to 2, 2
            maxX = 30;               //********************************************************** CHANGE ME LATER *************  
            maxY = 30;               //********************************************************** CHANGE ME LATER ************* 

            initGameData();   // Create or load data depending on option clicked
        }
        // Destroy duplicated gameObject created when changing scenes
        else
            DestroyImmediate(gameObject);
    }

    // Generate and adds all enemies
    void generateAllEnemies()
    {
        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                // 25% chance of spawning enemy on tile
                 if (Random.Range(0.0f, 1.0f) < 0.25f)
                 {
                      generateRandomEnemies(i, j);
                      mapGenerator.addTile(i, j, true);
                 }
                 else
                      mapGenerator.addTile(i, j, false);
            }
        }
    }

    // Generate random squad based on hex coordinates
    void generateRandomEnemies(int x, int y)
    {
        // Calculate the number unit for this squad based on coordinates
        //temporarily set to 4
        int squadSize = 4;       //********************************************************** CHANGE ME LATER *************                                    
        allEnemyUnits[x][y] = new List<UnitClass>(squadSize);

        for (int i = 0; i < squadSize; i++)
        {
            addNewUnit(false, i, 0, x, y);            
        }
    }

    // Adds a unit when called (e.g. start new game, recruit a new member)
    void addNewUnit(bool player, int classType, int battleSquad=1, int x=0, int y=0)
    {
        UnitClass newUnit = new UnitClass();
        nameSelector.getName();
        newUnit.changeName(nameSelector.firstName, nameSelector.lastName);
        newUnit.classType = classType;
        newUnit.classChange();

        if(player)
        {
            allPlayerUnits.Add(newUnit);
            print("Added Player: " + newUnit.firstName + " " + newUnit.lastName);     
        }
        else
        {
            setEnemyStats(ref newUnit, x, y);
            allEnemyUnits[x][y].Add(newUnit);
            print("Added Enemy!");
            //print("Added Enemy: " + newUnit.firstName + " " + newUnit.lastName);
        }
    }

    // Alters the stats of enemies depending on the hex coordinates
    void setEnemyStats(ref UnitClass unit, int x, int y)
    {
        unit.maxHealth += unit.maxHealth*(x+1); // temporary
        unit.currentHealth = unit.maxHealth;
    }

    // Load the game data and game state, if it fails return to start menu with en error
    void initGameData()
    {
         mapGenerator.begin();
        if (StartMenu.startNew)
        {
            // Initialize the 2D array 
            // For testing, Simulates autopopulating the player units with
            // starting amount of units when game is started
            for (int add = 0; add < initialPlayerUnitCount; add++) //********************************************************** CHANGE ME LATER *************    
            {
                addNewUnit(true, add, 0);
            }

            allEnemyUnits = new List<UnitClass>[maxX][];
            for (int i = 0; i < maxX; i++)
            {
                allEnemyUnits[i] = new List<UnitClass>[maxY];
            }

            generateAllEnemies();
            mapGenerator.instantiateTiles();
        }
        else
            GameStateManager.loadGameData();        
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

        if (allEnemyUnits[enemyXCoord][enemyYCoord] == null)//********************************************************** FIX ME LATER ************* 
            print("No enemies here!");
        else
        {
            foreach (UnitClass unit in allEnemyUnits[enemyXCoord][enemyYCoord])
            {
                unitsInBattle.Add(unit);
            }
        }

        return unitsInBattle;
    }

    // Temporary function to test battle
    public void TestBattle()  //********************************************************** DELETE ME LATER *************                                    
    {
        print("Entering battle!");
        SceneManager.LoadScene("LeonTest");
    }
}

