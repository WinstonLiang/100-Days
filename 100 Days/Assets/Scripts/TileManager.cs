using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public int TilesPerZone = 1;
    public int NumOfZones = 1;
    public GameObject EmptyTile;
    public GameObject EnemyTile;

    private Dictionary<Vector2, GameObject> MapTiles;

    void Start()
    {
        // Initialize MapTiles
        MapTiles = new Dictionary<Vector2, GameObject>();

        Init();
    }

    /// <summary>
    /// Fills MapTiles with TilesPerZone x NumOfZones tiles.
    /// The tiles must be touching at least 2 other tiles to be created.
    /// </summary>
    void Init()
    {
        // Insert starting node (0, 0)
        MapTiles[new Vector2(0, 0)] = MakeEmptyTile(0, 0);

        // Insert the first new tile to start the algorithm
        if (Random.Range(0, 1) <= 0.25f)
            MapTiles[new Vector2(1, 0)] = MakeEnemyTile(1, 0);
        else
            MapTiles[new Vector2(1, 0)] = MakeEmptyTile(1, 0);

        bool tileFound = false;
        for (int i = 0; i < NumOfZones; i++)
        {
            for (int j = 0; j < TilesPerZone; j++)
            {
                tileFound = true; // FIX to false, leaving true to avoid infinite loop until finished.
                while (!tileFound)
                {
                    //pick random tile from a surrounding tile, getconnections and delete from list.
                    //  after finished with tile, repopulate list with surrounding tiles again.
                }
            }
        }
    }

    List<Vector2> GetConnections(int x, int y)
    {
        List<Vector2> connectedTiles = new List<Vector2>();
        List<Vector2> surroundingTiles = new List<Vector2>
        {
            new Vector2(1, 0)
        };

        return connectedTiles;
    }

    /// <summary>
    /// Creates an empty tile and initializes, then returns the GameObject.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    GameObject MakeEmptyTile(int x, int y)
    {
        GameObject tileClone = (GameObject)Instantiate(EmptyTile);
        tileClone.GetComponent<Tile>().initialize(x, y, 1, false);
        return tileClone;
    }

    /// <summary>
    /// Creates an enemy tile and initializes, then returns the GameObject.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    GameObject MakeEnemyTile(int x, int y)
    {
        GameObject tileClone = (GameObject)Instantiate(EnemyTile);
        tileClone.GetComponent<Tile>().initialize(x, y, 1, true);
        return tileClone;
    }
}
