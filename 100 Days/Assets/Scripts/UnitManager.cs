using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable] 
public class UnitManager : MonoBehaviour
{     
    public List<UnitClass> allPlayerUnits = new List<UnitClass>(4);
    public List<List<UnitClass>> allEnemyUnits;
    public int initialPlayerUnitCount = 3;
    public int maxPlayers = 4;    // Total number of units of a side that participates in a battle at the same time
    public static bool DEBUG = false;

    // To be used by Battle script 
    public int battlingSquad = 0;   // 0 is the active battling squad - 1 are the reserves

    private TileManager tileManager;

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
            tileManager = GetComponent<TileManager>();

            initGameData();   // Create or load data depending on option clicked
        }
        // Destroy duplicated gameObject created when changing scenes
        else
            DestroyImmediate(gameObject);
    }
    
    // Generate and adds all enemies
    void generateAllEnemies()
    {
        int enemyIndex = 0;
        foreach (Vector2 tile in tileManager.MapTiles.Keys)
        {
            Tile currentTile = tileManager.MapTiles[tile].GetComponent<Tile>();

            // if the tile has an enemy, create an enemy squad of the appropriate rank
            if (currentTile._hasEnemy)
            {
                generateRandomEnemies(enemyIndex, currentTile._coords, currentTile._rank);
                currentTile._enemyIndex = enemyIndex;
                enemyIndex++;
            }
        }
    }

    // Generate random squad based on hex coordinates
    void generateRandomEnemies(int index, Vector2 coords, int rank)
    {
        // Calculate the number unit for this squad based on rank
        int squadSize = 4;       //********************************************************** CHANGE ME LATER IF NEEDED*************                                    
        allEnemyUnits.Add(new List<UnitClass>());
        allEnemyUnits[index] = new List<UnitClass>(squadSize);

        for (int i = 0; i < squadSize; i++)
        {
            addNewUnit(false, Random.Range(0, 4), 0, (int)coords.x, (int)coords.y, index);            
        }
    }

    // Adds a unit when called (e.g. start new game, recruit a new member)
    void addNewUnit(bool player, int classType, int battleSquad=1, int x=0, int y=0, int enemyIndex=0)
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
            allEnemyUnits[enemyIndex].Add(newUnit);
            print("Added Enemy!");
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
         //mapGenerator.begin();
        if (StartMenu.startNew)
        {
            // Initialize the 2D array 
            // For testing, Simulates autopopulating the player units with
            // starting amount of units when game is started
            for (int add = 0; add < initialPlayerUnitCount; add++) //********************************************************** CHANGE ME LATER *************    
            {
                addNewUnit(true, add, 0);
            }

            allEnemyUnits = new List<List<UnitClass>>();

            generateAllEnemies();
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

    /// <summary>
    /// Returns the number of units in battle squad.
    /// </summary>
    /// <returns></returns>
    public int GetBattleSquadCount()
    {
        return getBattlingSquad().Count;
    }

    /// <summary>
    /// Return the reserve units.
    /// </summary>
    /// <returns></returns>
    public List<UnitClass> getReserves()
    {
        List<UnitClass> unitsInReserve = new List<UnitClass>();

        foreach (UnitClass unit in allPlayerUnits)
        {
            if (unit.squad != battlingSquad)
            {
                unitsInReserve.Add(unit);
            }
        }
        return unitsInReserve;
    }

    /// <summary>
    /// Return the proper enemy set based on index of enemyList.
    /// </summary>
    /// <param name="enemyIndex"></param>
    /// <returns></returns>
    public List<UnitClass> getEnemySquad(int enemyIndex)
    {
        List<UnitClass> unitsInBattle = new List<UnitClass>(maxPlayers);

        foreach (UnitClass unit in allEnemyUnits[enemyIndex])
        {
            unitsInBattle.Add(unit);
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

