using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {

    public GameObject playerUnit;
    public List<UnitClass> allPlayerUnits;
    public int maxPlayers = 4;
    public int battlingSquad = 0;

	// Use this for initialization
	void Start ()
    {
        playerUnit = GameObject.Find("UnitClass");
        allPlayerUnits.Capacity = maxPlayers;

        for(int add = 0; add < allPlayerUnits.Capacity; add++)
        {
            allPlayerUnits.Add(playerUnit.GetComponent<UnitClass>());
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    //Return an int for the squad in battle
    public List<UnitClass> getBattlingSquad()
    {
        List<UnitClass> unitsInBattle = new List<UnitClass>(maxPlayers);

        foreach (UnitClass unit in allPlayerUnits)
        {
            if (unit.squad == battlingSquad)
            {
                unitsInBattle.Add(unit);
            }
        }
        return unitsInBattle;
    }
    // Returns the list of the squad with matching squad number
    public List<UnitClass> getPlayerUnits(int squadNum)
    {
        List<UnitClass> unitsInSquad = new List<UnitClass>(4);

        foreach(UnitClass unit in allPlayerUnits)
        {
            if (unit.squad == squadNum)
                unitsInSquad.Add(unit);
        }
        return unitsInSquad;
    }
}
