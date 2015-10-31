using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {

    public List<UnitClass> allPlayerUnits;

	// Use this for initialization
	void Start ()
    {
        allPlayerUnits.Capacity = 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
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
