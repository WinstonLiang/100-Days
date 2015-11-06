using UnityEngine;
using System.Collections;

public class DefenderClass : MonoBehaviour {
    public GameObject character;

    private UnitClass unit;

	// Use this for initialization
	void Start () 
    {
        character = GameObject.Find("Unit");
        unit = character.GetComponent<UnitClass>();
        unit.maxHealth = 200;
        unit.att = 5;
        unit.def = 10;
        unit.maxSpeed = 9;
        unit.maxPower = 50;	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
