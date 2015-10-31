using UnityEngine;
using System.Collections.Generic;

public class Battle : MonoBehaviour {

    public UnitManager unitManager; // Retrieve squad information
    public GameManegers gameManager; // Retrieve day information
    public bool day;
    public bool paused;
    public bool playerParticipate;
    public bool battleStarted;
    public List<UnitClass> playerUnits; // A list of playerUnits grabbed from unitManager
    public List<UnitClass> enemyUnits; // A list of enemyUntis grabbed from enemyManager
    private bool playerWon;
    private bool enemyWon;

	// Use this for initialization
	void Start ()
    {
        battleStarted = true;
        playerWon = false;
        enemyWon = false;
        paused = false;
        playerParticipate = true; // Set to true for testing
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!playerWon && !enemyWon)
        {
            checkPlayerUnits();
            checkEnemyUnits();

            checkDeadUnits();
            tickDown();
        }
	}

    // Check the unitManager for battling units
    void getBattleUnits()
    {
        List<UnitClass> allPlayerUnits = unitManager.getPlayerUnits();

        foreach(UnitClass unit in allPlayerUnits)
        {

        }
    }

    // Check if any player units are at tick 0
    void checkPlayerUnits()
    {

    }

    // Check if any enemy units are at tick 0
    void checkEnemyUnits()
    {

    }

    // Check if all player units or all enemy units are dead
    void checkDeadUnits()
    {
        enemyWon = playersUnitsDead();
        playerWon = enemyUnitsDead();
    }

    // Check if every player unit is dead
    bool playersUnitsDead()
    {
        return false;
    }

    // Check if every enemy unit is dead
    bool enemyUnitsDead()
    {
        return false;
    }

    // Decrement the tick for every unit
    void tickDown()
    {
        // Decrement the tick for attacks

        // Decrement the tick for abilities
    }
}
