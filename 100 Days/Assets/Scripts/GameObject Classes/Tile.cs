using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public int _x;
    public int _y;
    public int _rank;
    public bool _hasEnemy;

    public void initialize(int x, int y, int rank, bool hasEnemy)
    {
        _x = x;
        _y = y;
        _rank = rank;
        _hasEnemy = hasEnemy;
    }
}
