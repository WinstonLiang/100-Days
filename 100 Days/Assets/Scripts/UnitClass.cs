using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour 
{

    public string firstName;
    public string lastName;
    public int level = 1;
    public int health = 100;
    public int att = 5;
    public int def = 5;
    public int speed =10;
    public string ability;
    public int crit = 1;
    public int dogde = 1;
    public int substat = 0;
    public bool deadFlag = false;
    public int classType = 0;



	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame

    void changeStat(int statChange, ref int orginalStat)
    {
        orginalStat = orginalStat - statChange;
    }
	void Update () 
    {
	   
	}
}
