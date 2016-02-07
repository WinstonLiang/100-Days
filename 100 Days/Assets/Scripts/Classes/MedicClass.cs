using UnityEngine;
using System.Collections.Generic;

public class MedicClass : Classes {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower, healingCounter, maxHealingCounter;
    public bool receiveDmgModify; // Flag whether class has skills that modify dmg received by team

    // Use this for initialization
    void Start () 
    {     
    }
	
    public void ablilityHeal(UnitClass unit)
    {
        
        //unit.currentHealth +=
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
        unit.healingCounter = maxHealingCounter;
        unit.healing = true;
    }

    public override void ability1(List<UnitClass> units, int enemy, bool isPlayer)
    {

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
        // Self Healing
        float healAmount = unit.maxHealth * 0.05F;

        if (unit.healingCounter <= 0)
        {
            if (unit.currentHealth + healAmount > unit.maxHealth)
                unit.currentHealth = unit.maxHealth;
            else
                unit.currentHealth += (int)healAmount;
            unit.healingCounter = maxHealingCounter;
            print(unit.firstName + " self healed for " + (int)healAmount + " points!!");
        }
        else
            unit.healingCounter--;
    }
}
