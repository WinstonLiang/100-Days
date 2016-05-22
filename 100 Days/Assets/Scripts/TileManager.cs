using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public int TilesPerZone = 5;
    public int NumOfZones = 1;
    public float EncounterChance = 0.25f;
    public GameObject EmptyTile;
    public GameObject EnemyTile;

    private Dictionary<Vector2, GameObject> MapTiles;
    private Vector2 CurrentTile;
    private Transform nodeParent;
    private int CurrentRank = 1;
    private int InitialTiles = 2;

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
        //get nodeParent transform
        nodeParent = GameObject.Find("DragParent").transform;

        // Insert starting node (0, 0)
        MapTiles[new Vector2(0, 0)] = MakeEmptyTile(new Vector2(0, 0), 0);

        // Set as starting center for tile generation       
        // and insert the first new tile to start the algorithm         
        CurrentTile = new Vector2(1, 0);
        MakeRandomTile(CurrentTile, CurrentRank);

        bool tileFound = false;
        int randomTile = 0;
        Vector2 randomCoords;
        List<Vector2> surroundTiles;
        for (int i = 0; i < NumOfZones; i++)
        {
            for (int j = 0; j < TilesPerZone; j++)
            {
                // Skip two iterations to count two initial tiles
                if (InitialTiles > 0)
                {
                    InitialTiles--;
                    continue;
                }

                tileFound = false;
                surroundTiles = FillWithSurroundingTiles(CurrentTile);

                while (!tileFound)
                {
                    randomTile = Random.Range(0, surroundTiles.Count);
                    randomCoords = surroundTiles[randomTile];
                    if (GetConnections(CurrentTile + randomCoords) > 1)
                    {
                        CurrentTile += randomCoords;
                        MakeRandomTile(CurrentTile, CurrentRank);                                                
                        tileFound = true;
                    }
                    else
                        surroundTiles.RemoveAt(randomTile);

                    if (tileFound) break;
                }

            }

            CurrentRank++;
        }
    }

    /// <summary>
    /// Returns the number of tiles touching the tile at (x, y).
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    int GetConnections(Vector2 tile)
    {
        // if tile being checked is already occupied, skip this tile
        if (MapTiles.ContainsKey(tile))
            return 0;

        int tilesTouching = 0;
        List<Vector2> surroundingTiles = FillWithSurroundingTiles(tile);

        foreach (Vector2 surroundTile in surroundingTiles)
        {
            if (MapTiles.ContainsKey(tile + surroundTile))
                tilesTouching++;
        }

        return tilesTouching;
    }

    /// <summary>
    /// Returns a list with surrounding tiles.
    /// </summary>
    /// <returns>A list filled with Vector2.</returns>
    List<Vector2> FillWithSurroundingTiles(Vector2 tile)
    {
        if (tile.x % 2 == 0)
        {
            List<Vector2> surroundingTiles = new List<Vector2>
            {
                new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0),
                new Vector2(-1, -1), new Vector2(1, -1), new Vector2(0, 1)
            };
            return surroundingTiles;
        }
        else
        {
            List<Vector2> surroundingTiles = new List<Vector2>
            {
                new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0),
                new Vector2(-1, 1), new Vector2(1, 1), new Vector2(0, 1)
            };
            return surroundingTiles;
        }
    }

    #region Make Tiles
    /// <summary>
    /// Creates an empty or enemy tiles depending on encounter chance.
    /// </summary>
    /// <param name="tile"></param>
    void MakeRandomTile(Vector2 tile, int rank)
    {
        if (Random.Range(0.0f, 1.0f) <= EncounterChance)
            MapTiles[tile] = MakeEnemyTile(tile, rank);
        else
            MapTiles[tile] = MakeEmptyTile(tile, rank);
    }

    /// <summary>
    /// Creates an empty tile and initializes, then returns the GameObject.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    GameObject MakeEmptyTile(Vector2 tile, int rank)
    {
        GameObject tileClone = (GameObject)Instantiate(EmptyTile, new Vector3(tile.x * 1.05f, (-tile.y + (tile.x % 2) * .5f) * 1.12f, 0), Quaternion.identity);
        tileClone.GetComponent<Tile>().initialize(tile, rank, false);
        tileClone.transform.SetParent(nodeParent);

        if (UnitManager.DEBUG) print("Creating empty tile!");
        return tileClone;
    }

    /// <summary>
    /// Creates an enemy tile and initializes, then returns the GameObject.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    GameObject MakeEnemyTile(Vector2 tile, int rank)
    {
        GameObject tileClone = (GameObject)Instantiate(EnemyTile, new Vector3(
            tile.x * 1.05f, 
            (-tile.y + (tile.x % 2) * .5f) * 1.12f, 0), 
            Quaternion.identity);
        tileClone.GetComponent<Tile>().initialize(tile, rank, true);
        tileClone.transform.SetParent(nodeParent);

        if (UnitManager.DEBUG) print("Creating enemy tile!");
        return tileClone;
    }
    #endregion
}
