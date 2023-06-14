using System.Collections;
using System.Collections.Generic;
using Sokoban;
using UnityEngine;

public class Game : MonoBehaviour
{
    private List<List<Tile>> _map;
    private int _mapSize;

    public Game(int mapSize)
    {
        _mapSize = mapSize;
        _map = new List<List<Tile>>();

        for (int i = 0; i < mapSize; i++)
        {
            List<Tile> row = new List<Tile>();
            for (int j = 0; j < mapSize; j++)
            {
                row.Add(new Tile { X = i, Y = j });
            }
            _map.Add(row);
        }
    }

    public void SetWall(int x, int y)
    {
        _map[x][y].IsWall = true;
    }

    public void SetBonus(int x, int y)
    {
        _map[x][y].HasBonus = true;
    }

    public void SetMovableObject(int x, int y)
    {
        _map[x][y].HasMovableObject = true;
    }

    public bool MoveObject(int x1, int y1, int x2, int y2)
    {
        if (_map[x2][y2].IsWall || _map[x2][y2].HasMovableObject)
        {
            return false;
        }

        _map[x1][y1].HasMovableObject = false;
        _map[x2][y2].HasMovableObject = true;

        return true;
    }
}
