using UnityEngine;
using System.Collections;

public class DefenderClass : MonoBehaviour {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower;    

    // Use this for initialization
    void Start () 
    {              
         maxHealth = 200;
         att = 5;
         def = 10;
         maxSpeed = 9;
         maxPower = 50;	    
	}
	
	// Update is called once per frame
	void Update () {
	
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
