using UnityEngine;
using System.Collections.Generic;

public class AssaultClass : Classes {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower,
               totalAbilities;
    public bool human, receiveDmgModify; // Flag whether class has skills that modify dmg received by team     

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
        float chance = Random.Range(0.0f, 1.0f);
        if(chance <= 0.2f)
        {
            if(isPlayer)
            {
                print("Current speed of enemy " + units[enemy].firstName + ": " + units[enemy].currentSpeed);
                units[enemy].currentSpeed = units[enemy].maxSpeed;
                print("After speed of enemy " + units[enemy].firstName + ": " + units[enemy].currentSpeed);
            }
            else
            {
                units[enemy].currentSpeed = units[enemy].maxSpeed;
            }
        }        
    }

    public override void ability2(List<UnitClass> units, int enemy, bool isPlayer)
    {

    }

    public override void receiveDmgAbility1(List<UnitClass> units, UnitClass target, ref int ally, ref bool activated)
    {

    }

    public override bool returnReceiveDmgModify() 
    {
        return receiveDmgModify;
    }

    public override void classTick(UnitClass unit)
    {

    }
}
