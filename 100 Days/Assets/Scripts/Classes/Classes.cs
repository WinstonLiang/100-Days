using UnityEngine;
using System.Collections.Generic;

public class Classes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void classChange(UnitClass unit) {}
    public virtual void ability1(List<UnitClass> units, int enemy, bool isPlayer) { }
    public virtual void ability2(List<UnitClass> units, int enemy, bool isPlayer) { }
}
