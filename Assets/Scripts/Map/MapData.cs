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
    public int originX;
    public int originY;

    public CellType[] cells;

    public void SetCell(int x, int y, CellType type)
    {
        cells[y * width + x] = type;
    }

    public CellType GetCell(int x, int y)
    {
        return cells[y * width + x];
    }
    public CellType GetCellByTilemapCoord(int tilemapX, int tilemapY)
    {
        int localX = tilemapX - originX;
        int localY = tilemapY - originY;

        if (localX < 0 || localY < 0 || localX >= width || localY >= height)
            return CellType.Empty;

        return cells[localY * width + localX];
    }
}
