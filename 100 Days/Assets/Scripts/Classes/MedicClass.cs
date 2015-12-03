using UnityEngine;
using System.Collections;

public class MedicClass : MonoBehaviour {

    public Sprite idleSprite;
    public int maxHealth, att, def, maxSpeed, maxPower;    

    // Use this for initialization
    void Start () 
    {
         maxHealth = 100;
         att = 10;
         def = 7;
         maxSpeed = 20;
         maxPower = 40;
	}
	
    public void ablilityHeal(UnitClass unit)
    {
        
        //unit.currentHealth +=
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
