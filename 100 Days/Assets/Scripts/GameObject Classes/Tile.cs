using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public Vector2 _coords;
    public int _rank;
    public bool _hasEnemy;

    public void initialize(Vector2 coords, int rank, bool hasEnemy)
    {
        _coords = coords;
        _rank = rank;
        _hasEnemy = hasEnemy;
    }
}
