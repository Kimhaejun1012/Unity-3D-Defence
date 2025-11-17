using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Empty,
    Road,
    Buildable,
    Blocked
}

[CreateAssetMenu(fileName = "MapData", menuName = "DF/Map Data")]
public class MapData : ScriptableObject
{
    public int width;
    public int height;

    public CellType[] cells;

    public void SetCell(int x, int y, CellType type)
    {
        cells[y * width + x] = type;
    }

    public CellType GetCell(int x, int y)
    {
        return cells[y * width + x];
    }
}