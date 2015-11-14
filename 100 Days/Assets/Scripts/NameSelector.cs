using UnityEngine;
using System.Collections;

public class NameSelector : MonoBehaviour {

    public int gender = Random.Range(0, 2);
    string[] first = {"Alex","Christopher","David","Leon","William","Winston","Aaron","Alan","James","Daivs","Arnold","Victor","Chirs","Jake","George","Mark","Michael","Rodney","Jack",
                      "Tiffany", "Beth", "Alice", "Rhonda", "Tina", "Sarah","Chirstine","Kathy","Summer","Mary","Dawn","Wendy","Piper","Hillary"};
    string[] last = { "Chapp", "Walker", "Nguyen", "Perez","Liang","Loera","Birch","Gibson" ,"Wong","Walker"};
    string[] codeName = { "Bob The Builder", "Super Spaceman", "Lone Wanderer" };
    public string firstName;
    public string lastName;
    public string names;
	// Use this for initialization
	void Start () {

	}
    public void getName ()
    {
        firstName = first[Random.Range(0, first.Length)];    
        lastName = last[Random.Range(0, last.Length)];
        names = codeName[Random.Range(0, codeName.Length)];
    }
	
}
