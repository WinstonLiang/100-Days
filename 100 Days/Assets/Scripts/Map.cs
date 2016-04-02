using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Map : MonoBehaviour
{

     public GameObject maptile;
     public int size;
     public float originX;
     public float originY;
     private List<Vector3> tiles;


     // Use this for initialization
     void Start()
     {
          tiles = new List<Vector3>();
          for (float x = 0; x <= size; x++)
          {
               for (float y = 0; y <= size; y++)
               {
                    Vector3 pos = new Vector3(x + originX, -(y + originY) + (x % 2) * .5f, 0);
                    tiles.Add(pos);
               }
          }

          foreach (Vector3 T in tiles)
          {
               Instantiate(maptile, T, Quaternion.identity);
          }

     }

     public void addTile(int x, int y)
     {
          Vector3 pos = new Vector3(x + originX, -(y + originY) + (x % 2) * .5f, 0);
     }
}
