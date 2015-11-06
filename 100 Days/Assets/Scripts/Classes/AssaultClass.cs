using UnityEngine;
using System.Collections;

public class AssaultClass : MonoBehaviour {

    public GameObject character;

    private UnitClass unit;

	// Use this for initialization
	void Start () 
    {
        character = GameObject.Find("Unit");
        unit = character.GetComponent<UnitClass>();
        unit.maxHealth = 120;
        unit.att = 7;
        unit.def = 7;
        unit.maxSpeed = 12;
        unit.maxPower = 60;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
