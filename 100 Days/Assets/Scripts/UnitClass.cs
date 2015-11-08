using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitClass : MonoBehaviour 
{
    public List<int> stats;

    public GameObject unitName; //Get the unit name
    public GameObject unit; //The type of unit

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

    public int ablity = 0;

    private NameSelector name;
    private int gender = Random.Range(0, 2);

	// Use this for initialization
	void Start () 
    {
        unitName = GameObject.Find("Name");
        name = unitName.GetComponent<NameSelector>();
        unit = GameObject.Find("Classes");
        
        if (gender == 0) // Determine gender of unit
        {
            name.gender = "guy";
        }
        else
        {
            name.gender = "girl";
        }

        name.getName(); // Determine name of unit
        firstName = name.firstName;
        lastName = name.lastName;
	}

    // Change the stats of the unit
    void changeStat(int statChange, int orginalStat)
    {
        orginalStat = orginalStat - statChange;
    }

    // Change the class of the unit
    void classChange(int otherClass)
    {
        if (otherClass == 1)
        {
            AssaultClass type = unit.GetComponent<AssaultClass>();
        }
        else if (otherClass == 2)
        {
            DefenderClass type = unit.GetComponent<DefenderClass>();
        }
        else if (otherClass == 3)
        {
            MedicClass type = unit.GetComponent<MedicClass>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            deadFlag = true;
        }
        //classChange(1); //Test
    }
}
