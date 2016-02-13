using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class tile
{
     //parameter should also include a representation of a group of enemies
     public Vector3 tilePosition;
     public int terrain = 0;

     public bool visilibity;
     public bool battle;
     public bool squad = false;
}



public class Map : MonoBehaviour
{

     public GameObject maptile;
     public int size;
     public float originX;
     public float originY;
     private List<tile> tiles;


     // Use this for initialization
     void Start()
     {
          tiles = new List<tile>();
          for (float x = 0; x <= size; x++)
          {
               for (float y = 0; y <= size; y++)
               {
                    tile newTile = new tile();
                    Vector3 pos = new Vector3(x + originX, -(y + originY) + (x % 2) * .5f, 0);
                    newTile.tilePosition = pos;
                    tiles.Add(newTile);
               }
          }

          foreach (tile T in tiles)
          {
               Instantiate(maptile, T.tilePosition, Quaternion.identity);
          }

     }

     // Update is called once per frame
     void Update()
     {

     }
}
