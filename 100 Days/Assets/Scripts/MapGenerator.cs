﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class tile
{
     //parameter should also include a representation of a group of enemies
     public Vector3 tilePosition;
     public int terrain = 0;

     public bool visilibity;
     public bool battle = false;
     public bool squad = false;
     public List<UnitClass> tileEnemies = new List<UnitClass>(4);
}

public class MapGenerator : MonoBehaviour {

     private Dictionary<Vector3,tile> generatedMap;
     private Transform nodeParent;

     public int biomeSize;
     //public NameSelector nameSelector;
     public GameObject maptile;
     public GameObject maptile2;

	// Use this for initialization
     /*
	void Start () {
          Random.seed = (int)System.DateTime.Now.Ticks;
          generatedMap = new Dictionary<Vector3,tile>();
	     for (int x = 0; x <= biomeSize; x++)
               for (int y = 0; y <= biomeSize; y++)
               {
                    tile newTile = new tile();
                    newTile.tilePosition = new Vector3(x*1.05f,(-y  +(x % 2) * .5f) * 1.12f, 0);
                    if (Random.Range(0, 1f) < 0.25)
                    {
                         newTile.battle = true;
                         //
                         for (int add = 0; add < newTile.tileEnemies.Capacity; add++)
                         {
                              
                              UnitClass newUnit = new UnitClass();
                              nameSelector.getName();
                              newUnit.changeName(nameSelector.firstName, nameSelector.lastName);

                              // Temporarily change class to test sprites
                              newUnit.classType = 1;
                              newUnit.classChange(newUnit);

                              newTile.tileEnemies.Add(newUnit);
                              print("Added Enemy: " + newTile.tileEnemies[add].firstName + " " + newTile.tileEnemies[add].lastName);
                               
                         }
                          //
                    }
                    generatedMap.Add(newTile.tilePosition, newTile);
               }
          foreach (KeyValuePair<Vector3,tile> T in generatedMap)
          {
               if (T.Value.battle)
                    Instantiate(maptile2, T.Value.tilePosition, Quaternion.identity);
               else
                    Instantiate(maptile, T.Value.tilePosition, Quaternion.identity);
          }
	}
      */

     public void begin()
     {
          generatedMap = new Dictionary<Vector3, tile>();
     }

     public void instantiateTiles()
     {
         //get nodeParent transform
         nodeParent = GameObject.Find("DragParent").transform;

          foreach (KeyValuePair<Vector3, tile> T in generatedMap)
          {
              //declare the instantiated object
              GameObject newTile;

               if (T.Value.battle)
                    newTile = (GameObject)Instantiate(maptile2, T.Value.tilePosition, Quaternion.identity); // Battle tile
               else
                    newTile = (GameObject)Instantiate(maptile, T.Value.tilePosition, Quaternion.identity); // Empty tile

              //set the instantiated tile's parent to nodeParent
               newTile.transform.SetParent(nodeParent);
          }
     }

     public void addTile(int x, int y, bool enemy)
     {
          tile newTile = new tile();
          newTile.tilePosition = new Vector3(x * 1.05f, (-y + (x % 2) * .5f) * 1.12f, 0);
          newTile.battle = enemy;
          generatedMap.Add(newTile.tilePosition, newTile);
     }

}