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

    // Returns the list with all the units
    public List<UnitClass> getPlayerUnits()
    {
        return allPlayerUnits;
    }
}
