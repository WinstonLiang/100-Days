﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class UnitClass
{
    public int x, y;
    public string firstName, lastName;
    public int level;
    public int currentHealth, maxHealth;
    public int att, def;
    public int currentSpeed, maxSpeed;
    public int currentPower, maxPower;
    public int crit, dodge;
    public int substat;
    public bool deadFlag, healing;
    public int classType;
    public int squad;
    public int ablity;
    public int healingCounter;

    // Constructor
    public UnitClass()
    {
        level = 1;
        currentHealth = 100;
        maxHealth = 100;
        att = 10;
        def = 5;
        currentSpeed = 10;
        maxSpeed = 10;
        currentPower = 50;
        maxPower = 50;
        crit = 1;
        dodge = 1;
        substat = 0;
        deadFlag = false;
        classType = 0;
        squad = 0;
        ablity = 0;
        healing = false;


        firstName = "default firstname";
        lastName = "default lastname";
    }

    // Change the class of the unit
    public void classChange()
    {
        Classes classScript = getClassScript();
        classScript.classChange(this);
    }

    // Retrieves the reference to it's own class script
    public Classes getClassScript()
    {
        if (classType == 1)
        {
            return GameObject.Find("AssaultGO").GetComponent<AssaultClass>();
        }
        else if (classType == 2)
        {
            return GameObject.Find("DefenderGO").GetComponent<DefenderClass>();
        }
        else // Add more if statements for more classes (classType == 3)
        {
            return GameObject.Find("MedicGO").GetComponent<MedicClass>();
        }
    }

    // Change the stats of the unit (Useless function?)
    public void changeStat(int statChange, int orginalStat)
    {
        orginalStat = orginalStat - statChange;
    }

    // Change the name of the unit
    public void changeName(string first, string last)
    {
        firstName = first;
        lastName = last;
    }

    public string classToString()
    {
        if (classType == 1)
        {
            return "Assault";
        }
        else if (classType == 2)
        {
            return "Defender";
        }
        else // Add more if statements for more classes (classType == 3)
        {
            return "Medic";
        }
    }
}
