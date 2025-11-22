using UnityEngine;
using UnityEngine.Tilemaps;
public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public MapData mapData;

    public void BakeToMapData()
    {
        if (mapData == null)
        {
            Debug.LogError("MapData is Null");
            return;
        }

        if (tilemap == null)
        {
            Debug.LogError("Tilemap is Null");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;
        int width = bounds.size.x;
        int height = bounds.size.y;

        mapData.originX = bounds.xMin;
        mapData.originY = bounds.yMin;

        mapData.width = width;
        mapData.height = height;
        mapData.cells = new CellType[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3Int pos = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                TileBase tile = tilemap.GetTile(pos);

                CellType type;

                if (tile is RoadTile) type = CellType.Road;
                else if (tile is BuildableTile) type = CellType.Buildable;
                else if (tile is BlockedTile) type = CellType.Blocked;
                else type = CellType.Empty;

                mapData.SetCell(x, y, type);

                //Debug.Log($"{x}, {y} -> {type}");
            }
        }
        //Debug.Log($"Bake Good: {width} x {height}");
    }
}
