using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class tile
{
     //parameter should also include a representation of a group of enemies
     public Vector3 tilePosition;
     public int terrain = 0;

     private bool visilibity;
     private bool battle;
     private bool squad = false;
}



public class Map : MonoBehaviour {

     public GameObject maptile;
     public List<tile> tiles;

	// Use this for initialization
	void Start () {
          foreach (tile T in tiles)
          {
               Instantiate(maptile, T.tilePosition, Quaternion.identity);
          }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
