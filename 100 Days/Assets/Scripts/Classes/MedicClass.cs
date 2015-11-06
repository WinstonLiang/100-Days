using UnityEngine;
using System.Collections;

public class MedicClass : MonoBehaviour {
    public GameObject character;

    private UnitClass unit;
	// Use this for initialization
	void Start () 
    {
        character = GameObject.Find("Unit");
        unit = character.GetComponent<UnitClass>();
        unit.maxHealth = 100;
        unit.att = 10;
        unit.def = 7;
        unit.maxSpeed = 20;
        unit.maxPower = 40;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
