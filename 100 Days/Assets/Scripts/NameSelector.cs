using UnityEngine;
using System.Collections;

public class NameSelector : MonoBehaviour {

    public int gender;        
    public string firstName;
    public string lastName;
    public string names;

    public string[] first;
    public string[] last;
    public string[] codeName;

	// Use this for initialization
	void Start () 
    {
        gender = Random.Range(0, 2);
        first = new string[]{ "Alex", "Christopher", "David", "Leon", "William", "Winston", "Aaron", "Alan", "James", "Davis", "Arnold", "Victor", "Chris", "Jake", "George", "Mark", "Michael", 
                             "Rodney", "Jack", "Tiffany", "Beth", "Alice", "Rhonda", "Tina", "Sarah", "Christine", "Kathy", "Summer", "Mary", "Dawn", "Wendy", "Piper", "Hillary" };
        last = new string[]{ "Chapp", "Walker", "Nguyen", "Perez", "Liang", "Loera", "Birch", "Gibson", "Wong", "Walker" };
        codeName = new string[]{ "Bob The Builder", "Super Spaceman", "Lone Wanderer" };
	}

    public void getName ()
    {
        firstName = first[Random.Range(0, first.Length)];
        lastName = last[Random.Range(0, last.Length)];
        names = codeName[Random.Range(0, codeName.Length)];
    }
	
}
