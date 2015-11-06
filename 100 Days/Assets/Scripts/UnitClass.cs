using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitClass : MonoBehaviour 
{
    public List<int> stats;

    public GameObject characterName;
    public GameObject character;

    public string firstName;
    public string lastName;
    public int level = 1;
    public int currentHealth = 100;
    public int maxHealth = 100;
    public int att = 5;
    public int def = 5;
    public int currentSpeed = 10;
    public int maxSpeed = 10;
    public int currentPower = 50;
    public int maxPower = 50;
    public int crit = 1;
    public int dodge = 1;
    public int substat = 0;
    public bool deadFlag = false;
    public int classType = 0;
    public int squad = 0;

    private NameSelector name;
    private int gender = Random.Range(0, 2);

	// Use this for initialization
	void Start () 
    {
        characterName = GameObject.Find("Name");
        name = characterName.GetComponent<NameSelector>();
        character = GameObject.Find("Player");
        if (gender == 0)
        {
            name.gender = "guy";
        }
        else
        {
            name.gender = "girl";
        }
        name.getName();
        firstName = name.firstName;
        lastName = name.lastName;
	}
	
	// Update is called once per frame

    void changeStat(int statChange, ref int orginalStat)
    {
        orginalStat = orginalStat - statChange;
    }

    void classChange(int otherClass)
    {
        if (otherClass == 0)
        {
            AssaultClass type = character.GetComponent<AssaultClass>();
        }
        else if (otherClass == 1)
        {
            DefenderClass type = character.GetComponent<DefenderClass>();
        }
        else if (otherClass == 2)
        {
            MedicClass type = character.GetComponent<MedicClass>();
        }
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            deadFlag = true;
        }
        classChange(1); //Test
    }
}
