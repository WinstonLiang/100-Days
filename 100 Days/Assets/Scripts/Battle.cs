using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour {
        
    public bool day;
    public bool paused;
    public bool playerParticipate;
    public bool currentlyBattling;
    public float tickTime; // Length of each tick time
    public List<UnitClass> playerUnits; // A list of playerUnits grabbed from unitManager
    public GameObject P1, P2, P3, P4; // Temporarily set to public for testing
    public List<UnitClass> enemyUnits; // A list of enemyUnits grabbed from enemyManager
    public GameObject E1, E2, E3, E4; // Temporarily set to public for testing 
    public float time;
    public int battleLength;

    GameObject unitsData; // To retrieve other scripts
    UnitManager unitManager; // Retrieve squad information
    GameManagers gameManager; // Retrieve day information
    AssaultClass assaultScript;
    DefenderClass defenderScript;
    MedicClass medicScript;
    bool playerWon;
    bool enemyWon;    
    int squadInBattle = 1; // Change to 0 after unitmanager is implemeneted    

	// Use this for initialization
	void Start ()
    {
        // Using delayed invoke is not necessary, can change script execution order in Unity/Project Settings
        currentlyBattling = true;
        playerWon = false;
        enemyWon = false;        
        paused = false;
        playerParticipate = true; // Set to true for testing
        unitsData = GameObject.Find("UnitsData");
        unitManager = unitsData.GetComponent<UnitManager>();
        gameManager = unitsData.GetComponent<GameManagers>();
        assaultScript = GetComponent<AssaultClass>();
        defenderScript = GetComponent<DefenderClass>();
        medicScript = GetComponent<MedicClass>();
        playerUnits = unitManager.getBattlingSquad();
        enemyUnits = unitManager.getEnemySquad();
        setSlots();
        setSprites();      
    }

	// Update is called once per frame
	void Update ()
    {
        if (currentlyBattling && !playerWon && !enemyWon)
        {
            checkPlayerUnits();
            checkEnemyUnits();
            checkDeadUnits();
            tickDown();
            checkBattleEnd();         
        }
        else
        {
            currentlyBattling = false;
        }
	}

    // Set the sprite depending on the class type
    void changeSprite(SpriteRenderer renderer, int classType)
    {
        if (classType == 0)
        {
            // Set Apprentice/Infantry sprite here
        }
        else if (classType == 1)
        {
            renderer.sprite = assaultScript.idleSprite;
        }
        else if (classType == 2)
        {
            renderer.sprite = defenderScript.idleSprite;
        }
        else if (classType == 3)
        {
            renderer.sprite = medicScript.idleSprite;
        }
    }

    // Set the reference of the Player annd Enemy GameObjects 
    void setSlots()
    {
        P1 = GameObject.Find("P1");
        P2 = GameObject.Find("P2");
        P3 = GameObject.Find("P3");
        P4 = GameObject.Find("P4");

        E1 = GameObject.Find("E1");
        E2 = GameObject.Find("E2");
        E3 = GameObject.Find("E3");
        E4 = GameObject.Find("E4");
    }

    // Sets the player and enemy sprites depending on their classes
    void setSprites()
    {
        GameObject[] playerSlots = { P1, P2, P3, P4 };
        GameObject[] enemySlots = { E1, E2, E3, E4 };

        for (int i = 0; i < playerSlots.Length; i++)
        {
            print("Adding sprite");
            changeSprite(playerSlots[i].GetComponent<SpriteRenderer>(), playerUnits[i].classType);
            changeSprite(enemySlots[i].GetComponent<SpriteRenderer>(), enemyUnits[i].classType);
        }
    }
       
    // Check if any player units attack/ability are at tick 0, if they are dead or not
    void checkPlayerUnits()
    {       
        foreach(UnitClass unit in playerUnits)
        {
            // Check if the unit is alive
            if (unit.currentHealth <= 0)
            {
                if(!unit.deadFlag)
                {
                    print("(Player) " + unit.firstName + " " + unit.lastName + " has died..");
                    unit.currentHealth = 0;
                    unit.currentSpeed = 0;
                    unit.currentPower = 0;
                    unit.deadFlag = true;
                }                
                continue;
            }

            // Check the attack tick
            if (unit.currentSpeed == 0)
            {                
                // Do attacking here **********************************
                unitAttack(unit, true);
                unit.currentSpeed = unit.maxSpeed;
            }

            // Check the ability tick
            if(unit.currentPower == 0)
            {
                // Do ability attack here **********************************
                print(unit.firstName + " uses ability!");
                unit.currentPower = unit.maxPower;
            }
        }
    }

    // Check if any enemy units attack/ability are at tick 0, if they are dead or not
    void checkEnemyUnits()
    {
        foreach (UnitClass unit in enemyUnits)
        {
            // Check if the unit is alive
            if (unit.currentHealth <= 0)
            {
                if(!unit.deadFlag)
                {
                    print("(Enemy) " + unit.firstName + " " + unit.lastName + " has been eliminated!");
                    unit.currentHealth = 0;
                    unit.currentSpeed = 0;
                    unit.currentPower = 0;
                }                
                unit.deadFlag = true;
                continue;
            }

            // Check the attack tick
            if (unit.currentSpeed == 0)
            {
                // Do attacking here **********************************                
                unitAttack(unit, false);
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

    // Performs the attack of the unit, boolean is set true for player and false for enemy
    void unitAttack(UnitClass unit, bool isPlayer)
    {
        int randomTarget;
        int attackDamage;
        List<UnitClass> allUnits;

        if (isPlayer)
        {
            allUnits = enemyUnits;
            randomTarget = getRandomEnemy();
        }
        else
        {
            allUnits = playerUnits;
            randomTarget = getRandomPlayer();
        }
        
        attackDamage = (int)(unit.att * Random.Range(2 / 3f, 4 / 3f)); // Temporarily use this range instead of dexterity
        allUnits[randomTarget].currentHealth -= attackDamage;
        print(unit.firstName + " attacks " + allUnits[randomTarget].firstName + " for " + attackDamage + " damage!");
    }

    // Check if all player units or all enemy units are dead
    void checkDeadUnits()
    {
        playerWon = enemyUnitsDead();
        if(!playerWon)
            enemyWon = playersUnitsDead();        
    }

    // Check if every player unit is dead
    bool playersUnitsDead()
    {
        foreach(UnitClass unit in playerUnits)
        {
            if (!unit.deadFlag)
                return false;
        }
        print("Player is defeated..");
        return true;
    }

    // Check if every enemy unit is dead
    bool enemyUnitsDead()
    {
        foreach (UnitClass unit in enemyUnits)
        {
            if (!unit.deadFlag)
                return false;
        }
        print("Player is victorious!!");
        return true; 
    }

    // Get a random player that is alive
    int getRandomPlayer()
    {
        int index = 0;
        while(true)
        {
            index = Random.Range(0, 4);
            if (!playerUnits[index].deadFlag)
                return index;
        }        
    }

    // Get a random enemy that is alive
    int getRandomEnemy()
    {
        int index = 0;
        while (true)
        {
            index = Random.Range(0, 4);
            if (!enemyUnits[index].deadFlag)
                return index;
        }
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
            battleLength--;
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
            //print("Battlelength: " + battleLength);
        }        
    }

    void checkBattleEnd()
    {
         if (battleLength <= 0)
         {
             currentlyBattling = false;
             print("Battle is over!!");
         }
    }
}
