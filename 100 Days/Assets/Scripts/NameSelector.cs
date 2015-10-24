using UnityEngine;
using System.Collections;

public class NameSelector : MonoBehaviour {

    public string gender;
    string[] guys= {"Alex","Christopher","David","Leon","William","Winston","Aaron","Alan","James","Daivs","Arnold","Victor","Chirs","Jake","George","Mark","Michael","Rodney","Jack"};
    string[] girls = { "Tiffany", "Beth", "Alice", "Rhonda", "Tina", "Sarah","Chirstine","Kathy","Summer","Mary","Dawn","Wendy","Piper","Hillary"};
    string[] last = { "Chapp", "Walker", "Nguyen", "Perez","Liang","Loera","Birch","Gibson" ,"Wong","Walker"};
    string firstName;
    string lastName;
    public string name;
	// Use this for initialization
	void Start () {
        if (gender == "guy"){
        firstName = guys[Random.Range(0, guys.Length)];}
        if (gender == "girl"){
        firstName = girls[Random.Range(0, guys.Length)];}
        lastName = last[Random.Range(0, last.Length)];
        name = (firstName +  ' ' + lastName);
	}
	
}
