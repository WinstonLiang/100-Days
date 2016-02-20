using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Battle : MonoBehaviour {
        
    public bool day;
    public bool paused;
    public bool playerParticipate;
    public bool currentlyBattling;
    public float tickTime; // Length of each tick time
    public List<UnitClass> playerUnits; // A list of playerUnits grabbed from unitManager
    public List<UnitClass> enemyUnits; // A list of enemyUnits grabbed from enemyManager
    public float time;
    public int battleLength;

    GameObject unitsData; // To retrieve other scripts
    UnitManager unitManager; // Retrieve squad information
    GameManagers gameManager; // Retrieve day information
    BattleUI battleUI; // Update battle UI
    
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
        battleUI = GetComponent<BattleUI>();

        playerUnits = unitManager.getBattlingSquad();
        enemyUnits = unitManager.getEnemySquad(); 
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
                unit.getClassScript(unit.classType).ability1(enemyUnits, getRandomEnemy(), true);
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
                    unit.deadFlag = true;
                }                                
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
                unit.getClassScript(unit.classType).ability1(playerUnits, getRandomPlayer(), true);
            }
        }
    }

    // Performs the attack of the unit, boolean is set true for player and false for enemy
    void unitAttack(UnitClass unit, bool isPlayer)
    {
        int randomTarget;
        int attackDamage;
        bool activated = false;
        List<UnitClass> allUnits;

        // Set the allUnits variable as appropriate and a random target randomly
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

        // Check if there is a living class that can modify damage received (e.g. defender)
        foreach (UnitClass targetUnit in allUnits)
        {
            if (!targetUnit.deadFlag && targetUnit != allUnits[randomTarget] && !activated && 
                targetUnit.getClassScript(targetUnit.classType).returnReceiveDmgModify())
            {
                targetUnit.getClassScript(targetUnit.classType).receiveDmgAbility1(allUnits, targetUnit, ref randomTarget, ref activated);
            }
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
            index = Random.Range(0, playerUnits.Count);
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
            index = Random.Range(0, enemyUnits.Count);
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
                if(!unitP.deadFlag)
                {
                    // Decrement the tick for attacks
                    unitP.currentSpeed -= 1;

                    // Decrement the tick for abilities
                    unitP.currentPower -= 1;

                    // Decrement the tick for class
                    unitP.getClassScript(unitP.classType).classTick(unitP);
                }
            }

            foreach (UnitClass unitE in enemyUnits)
            {
                if(!unitE.deadFlag)
                {
                    // Decrement the tick for attacks
                    unitE.currentSpeed -= 1;

                    // Decrement the tick for abilities
                    unitE.currentPower -= 1;

                    // Decrement the tick for class
                    unitE.getClassScript(unitE.classType).classTick(unitE);
                }
            }
            battleUI.updateBattleUI();
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
