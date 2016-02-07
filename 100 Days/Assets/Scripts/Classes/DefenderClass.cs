using UnityEngine;
using System.Collections.Generic;

public class DefenderClass : Classes {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower;
    public bool receiveDmgModify; // Flag whether class has skills that modify dmg received by team

    // Use this for initialization
    void Start () 
    {              
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void classChange(UnitClass unit)
    {
        unit.maxHealth = maxHealth;
        unit.currentHealth = maxHealth;
        unit.att = att;
        unit.def = def;
        unit.currentSpeed = maxSpeed;
        unit.currentPower = maxPower;
        unit.maxSpeed = maxSpeed;
        unit.maxPower = maxPower;
    }

    public override void ability1(List<UnitClass> units, int enemy, bool isPlayer)
    {

    }

    public override void ability2(List<UnitClass> units, int enemy, bool isPlayer)
    {

    }

    public override void receiveDmgAbility1(List<UnitClass> units, UnitClass target, ref int ally, ref bool activated)
    {
        if(Random.Range(0.0F, 1.0F) <= 0.1F)
        {
            // Defend Allies - 10% chance
            print("-------------------------" + target.firstName + " defended ally " + units[ally].firstName + "!!!!");
            for (int i = 0; i < 4; i++)
            {
                if (units[i] == target)
                {
                    ally = i;
                    activated = true;
                    break;
                }
            }  
        }     
    }

    public override bool returnReceiveDmgModify()
    {
        return receiveDmgModify;
    }

    public override void classTick(UnitClass unit)
    {

    }
}
