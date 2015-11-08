using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {

    public UnitManager unitManager; // Retrieve squad information
    public GameManagers gameManager; // Retrieve day information
    public Transform playerSlots; // To access the Player Units children
    public bool day;
    public bool paused;
    public bool playerParticipate;
    public bool battleStarted;
    public float tickTime; // Length of each tick time
    public List<UnitClass> playerUnits; // A list of playerUnits grabbed from unitManager
    public GameObject P1, P2, P3, P4; // Temporarily set to public for testing
    public List<UnitClass> enemyUnits; // A list of enemyUntis grabbed from enemyManager
    private bool playerWon;
    private bool enemyWon;    
    private int squadInBattle = 1; // Change to 0 after unitmanager is implemeneted
    private float time;
    //Add battle timer;

	// Use this for initialization
	void Start ()
    {
        battleStarted = true;
        playerWon = false;
        enemyWon = false;
        paused = false;       
        playerParticipate = true; // Set to true for testing
        unitManager = GetComponent<UnitManager>();
        gameManager = GetComponent<GameManagers>();
        playerUnits = unitManager.getPlayerUnits(1);
        setPlayerSlots();
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

    // Set the reference of the Player GameObjects 
    void setPlayerSlots()
    {
        P1 = GameObject.Find("P1");
        P2 = GameObject.Find("P2");
        P3 = GameObject.Find("P3");
        P4 = GameObject.Find("P4");
    }

    // Check if any player units attack/ability are at tick 0
    void checkPlayerUnits()
    {
        foreach(UnitClass unit in playerUnits)
        {
            // Check the attack tick
            if(unit.currentSpeed == 0)
            {
                // Do attacking here **********************************
                unit.currentSpeed = unit.maxSpeed;
            }

            // Check the ability tick
            if(unit.currentPower == 0)
            {
                // Do ability attack here **********************************
                unit.currentPower = unit.maxPower;
            }
        }
    }

    // Check if any enemy units attack/ability are at tick 0
    void checkEnemyUnits()
    {
        foreach (UnitClass unit in enemyUnits)
        {
            // Check the attack tick
            if (unit.currentSpeed == 0)
            {
                // Do attacking here **********************************
                unit.currentSpeed = unit.maxSpeed;
            }

            // Check the ability tick
            if (unit.currentPower == 0)
            {
                // Do ability attack here **********************************
                unit.currentPower = unit.maxPower;
            }
        }
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
        foreach(UnitClass unit in playerUnits)
        {
            if (!unit.deadFlag)
                return false;
        }
        return true;
    }

    // Check if every enemy unit is dead
    bool enemyUnitsDead()
    {
        return false;
    }

    // Decrement the tick for every unit
    void tickDown()
    {
        if(time < tickTime)
        {
            time += 0.1f;
        }
        else
        {
            time = 0.0f;
            foreach (UnitClass unitP in playerUnits)
            {
                // Decrement the tick for attacks
                unitP.currentSpeed -= 1;

                // Decrement the tick for abilities
                unitP.currentPower -= 1;
            }

            foreach (UnitClass unitE in enemyUnits)
            {
                // Decrement the tick for attacks
                unitE.currentSpeed -= 1;

                // Decrement the tick for abilities
                unitE.currentPower -= 1;
            }
        }
        Debug.Log("Tick time: " + time);
    }
}
