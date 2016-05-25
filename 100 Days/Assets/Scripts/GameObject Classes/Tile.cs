using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tile : MonoBehaviour
{
    public Vector2 _coords;
    public int _rank;
    public bool _hasEnemy;
    public int _enemyIndex;
    public Terrain _terrain;
    public bool _cleared;

    /// <summary>
    /// Constructs a tile object.
    /// </summary>
    public Tile()
    {
    }

    public void initialize(Vector2 coords, int rank, bool hasEnemy)
    {
        _coords = coords;
        _rank = rank;
        _hasEnemy = hasEnemy;
        _terrain = Terrain.Land;
        _cleared = false;
    }

    public void initialize(Vector2 coords, int rank, bool hasEnemy, Terrain terrain)
    {
        _coords = coords;
        _rank = rank;
        _hasEnemy = hasEnemy;
        _terrain = terrain;
        _cleared = false;
    }

    /// <summary>
    /// Initializes the tile object with every parameter of its class.
    /// </summary>
    /// <param name="coords"></param>
    /// <param name="rank"></param>
    /// <param name="hasEnemy"></param>
    /// <param name="enemyIndex"></param>
    /// <param name="terrain"></param>
    /// <param name="cleared"></param>
    public void initializeAll(Vector2 coords, int rank, bool hasEnemy, int enemyIndex, Terrain terrain, bool cleared)
    {
        _coords = coords;
        _rank = rank;
        _hasEnemy = hasEnemy;
        _enemyIndex = enemyIndex;
        _terrain = terrain;
        _cleared = cleared;
    }

    /// <summary>
    /// Transfer the variables that need to be carried over when the game is saved.
    /// </summary>
    /// <param name="tile"></param>
    public void ReplaceAttributes(int enemyIndex, bool cleared)
    {
        _enemyIndex = enemyIndex;
        _cleared = cleared;
    }
}

public enum Terrain
{
    Land,
    Mountain
};
