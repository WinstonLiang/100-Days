using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameStateManager : MonoBehaviour 
{    
    // All the data that needs to be saved goes in here
    [System.Serializable]
    public class AllUnits
    {
        public List<UnitClass> allPlayerUnits;
        public List<List<UnitClass>> allEnemyUnits;
        public List<DictSerializer> allMapTiles;
    }

    public void saveGameData()
    {
        // Fill in allUnits class using data from unitManagerScript
        GameObject unitsData = GameObject.Find("UnitsData");
        UnitManager unitManagerScript = unitsData.GetComponent<UnitManager>();
        TileManager tileManagerScript = unitsData.GetComponent<TileManager>();
        AllUnits allUnits = new AllUnits();
        allUnits.allPlayerUnits = unitManagerScript.allPlayerUnits;
        allUnits.allEnemyUnits = unitManagerScript.allEnemyUnits;
        allUnits.allMapTiles = VectorToCustom(tileManagerScript.MapTiles);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.dat");
        bf.Serialize(file, allUnits);
        file.Close();

        // Set PlayerPrefs saved flag to enable continue button on start menu
        PlayerPrefs.SetInt("saves", 1);
        print("Saved data.");
    }

    public void loadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.dat"))
        {
            GameObject unitsData = GameObject.Find("UnitsData");
            UnitManager unitManagerScript = unitsData.GetComponent<UnitManager>();
            TileManager tileManagerScript = unitsData.GetComponent<TileManager>();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.dat", FileMode.Open);
            AllUnits allunits = (AllUnits)bf.Deserialize(file);
            file.Close();

            // Load the data from AllUnits class
            unitManagerScript.allPlayerUnits = allunits.allPlayerUnits;
            unitManagerScript.allEnemyUnits = allunits.allEnemyUnits;
            tileManagerScript.MapTiles = CustomToVector(allunits.allMapTiles);
            print("Savefile found, loading...");
        }
        else
        {
            print("No savefile found.");
            SceneManager.LoadScene("StartMenu");
        }            
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    #region Serializer Helper Functions
    [System.Serializable]
    public class Vector2Serializer
    {
        public float _x, _y;

        public Vector2Serializer(float x, float y)
        {
            _x = x;
            _y = y;
        }
    }

    [System.Serializable]
    public class DictSerializer
    {
        public Vector2Serializer _serializer;
        public TileSerializer _tileSerializer;

        public DictSerializer(Vector2Serializer serializer, TileSerializer tileSerializer)
        {
            _serializer = serializer;
            _tileSerializer = tileSerializer;
        }
    }

    [System.Serializable]
    public class TileSerializer
    {
        public Vector2Serializer _coords;
        public int _rank;
        public bool _hasEnemy;
        public int _enemyIndex;
        public Terrain _terrain;
        public bool _cleared;

        public TileSerializer(Tile tile)
        {
            _coords = new Vector2Serializer(tile._coords.x, tile._coords.y);
            _rank = tile._rank;
            _hasEnemy = tile._hasEnemy;
            _enemyIndex = tile._enemyIndex;
            _terrain = tile._terrain;
            _cleared = tile._cleared;
        }
    }

    /// <summary>
    /// Converts unserializable mapTiles and returns a serializable customTiles.
    /// </summary>
    /// <param name="mapTiles"></param>
    /// <returns></returns>
    List<DictSerializer> VectorToCustom(Dictionary<Vector2, GameObject> mapTiles)
    {
        List<DictSerializer> customTiles = new List<DictSerializer>();

        foreach (Vector2 coords in mapTiles.Keys)
        {
            customTiles.Add(new DictSerializer(new Vector2Serializer(coords.x, coords.y),
                                                new TileSerializer(mapTiles[coords].GetComponent<Tile>())));
        }
        return customTiles;
    }

    /// <summary>
    /// Converts serializable customTiles and returns an unserializable mapTiles.
    /// </summary>
    /// <param name="customTiles"></param>
    /// <returns></returns>
    Dictionary<Vector2, GameObject> CustomToVector(List<DictSerializer> customTiles)
    {
        Dictionary<Vector2, GameObject> mapTiles = new Dictionary<Vector2, GameObject>();
        TileManager tileManager = GameObject.Find("UnitsData").GetComponent<TileManager>();
        Transform nodeParent = GameObject.Find("DragParent").transform;

        foreach (DictSerializer c in customTiles)
        {
            Vector2 coords = new Vector2(c._serializer._x, c._serializer._y);
            TileSerializer tileSerializer = c._tileSerializer;

            if (tileSerializer._hasEnemy)
                mapTiles[coords] = tileManager.MakeEnemyTile(coords, tileSerializer._rank, nodeParent, tileSerializer._enemyIndex, tileSerializer._cleared);
            else if (tileSerializer._terrain == Terrain.Mountain)
                mapTiles[coords] = tileManager.MakeImpassableTile(coords, tileSerializer._rank, nodeParent, tileSerializer._enemyIndex, tileSerializer._cleared);
            else
                mapTiles[coords] = tileManager.MakeEmptyTile(coords, tileSerializer._rank, nodeParent, tileSerializer._enemyIndex, tileSerializer._cleared);
        }
        return mapTiles;
    }
    #endregion
}