using UnityEngine;
using System.Collections;

public class AssaultClass : MonoBehaviour {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower;    

	// Use this for initialization
	void Start () 
    {        
        maxHealth = 120;
        att = 7;
        def = 7;
        maxSpeed = 12;
        maxPower = 60;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void classChange(UnitClass unit)
    {
        unit.maxHealth = maxHealth;
        unit.att = att;
        unit.def = def;
        unit.maxSpeed = maxSpeed;
        unit.maxPower = maxPower;
    }
}
