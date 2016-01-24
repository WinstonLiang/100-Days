using UnityEngine;
using System.Collections.Generic;

public class DefenderClass : Classes {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower;    

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
        unit.att = att;
        unit.def = def;
        unit.maxSpeed = maxSpeed;
        unit.maxPower = maxPower;
    }

    public override void ability1(List<UnitClass> units, int enemy, bool isPlayer)
    {

    }

    public override void ability2(List<UnitClass> units, int enemy, bool isPlayer)
    {

    }
}
