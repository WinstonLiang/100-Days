using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

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
     public string mapFile;
     public float originX;
     public float originY;
     private List<tile> tiles;

          
	// Use this for initialization
	void Start () {
          tiles = new List<tile>();
          string line;
          using (StreamReader file = new StreamReader(Application.dataPath + "/maps/" + mapFile)){
               while ((line = file.ReadLine()) != null)
               {
                    string[] mapData = line.Split(';');
                    tile newTile = new tile();
                    Vector3 pos = new Vector3(float.Parse(mapData[0])+originX,
                         -(float.Parse(mapData[1])+originY+(float.Parse(mapData[0])%2)*.5f),0);
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
	void Update () {
	
	}
}
