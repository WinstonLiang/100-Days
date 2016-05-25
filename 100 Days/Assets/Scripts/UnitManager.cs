using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private GameStateManager gameStateManager;
    private TileManager tileManager;
    private NameSelector nameSelector;
    public GameObject tileInfo;
    public Vector2 currentUserTile; // CHANGE TO PRIVATE LATER, SET TO PUBLIC FOR TESTING-************************************

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
            tileInfo = GameObject.Find("Tile Info");            

            initGameData();   // Create or load data depending on option clicked
        }
        // Destroy duplicated gameObject created when changing scenes
        else
            DestroyImmediate(gameObject);
    }

    #region Initialize
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

            tileManager.Init();
            currentUserTile = new Vector2(0, 0);
            allEnemyUnits = new List<List<UnitClass>>();
            generateAllEnemies();
        }
        else
        {
            gameStateManager = GameObject.Find("MenuCanvas").GetComponent<GameStateManager>();
            gameStateManager.loadGameData();
        }            
    }
    #endregion

    #region Unit generation
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
    #endregion

    #region Get Squad Functions
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
    #endregion

    #region Tile info window
    public void OpenTileInfo()
    {
        GameObject currentTile = EventSystem.current.currentSelectedGameObject;
        tileInfo.SetActive(true);
        tileInfo.transform.position = currentTile.transform.position + new Vector3(1, 1, 0);
        SetTileInfo(currentTile.transform);
    }

    void SetTileInfo(Transform currentTile)
    {
        Tile tile = currentTile.GetComponent<Tile>();
        string tileStatus = "Unexplored ";
        // check the status of this tile
        if (tile._hasEnemy)
            tileStatus = "Occupied ";
        else if (tile._cleared)
            tileStatus = "Controlled ";
        else if (tile._terrain == Terrain.Mountain)
            tileStatus = "Impassable ";

        tileInfo.transform.GetChild(0).GetComponent<Text>().text = tileStatus + tile._terrain.ToString();
        tileInfo.transform.GetChild(1).GetComponent<Text>().text = tile._hasEnemy ? allEnemyUnits[tile._enemyIndex].Count.ToString() : "0";
    }
    #endregion

    // Temporary function to test battle
    public void TestBattle()  //********************************************************** DELETE ME LATER *************                                    
    {
        print("Entering battle!");
        SceneManager.LoadScene("LeonTest");
    }
}

