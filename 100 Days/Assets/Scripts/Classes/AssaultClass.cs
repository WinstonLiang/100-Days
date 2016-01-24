using UnityEngine;
using System.Collections.Generic;

public class AssaultClass : Classes {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower,
               totalAbilities;
    public bool human;    

	// Use this for initialization
	void Start () 
    {        
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public override void classChange(UnitClass unit)
    {
        unit.maxHealth = maxHealth;
        unit.att = att;
        unit.def = def;
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
}
