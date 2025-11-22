using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;
    public MapData mapData;

    public Grid grid;

    private void Awake()
    {
        if (grid == null && tilemap != null)
            grid = tilemap.layoutGrid;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    void TryPlaceTower()
    {
        if (cam == null || tilemap == null || mapData == null || grid == null)
        {
            Debug.LogWarning("TowerPlacer 세팅이 안 됨");
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(grid.transform.up, grid.transform.position);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 worldPos = ray.GetPoint(enter);

            Vector3Int cellPos = grid.WorldToCell(worldPos);

            Debug.Log($"월드좌표: {worldPos}");
            Debug.Log($"셀좌표(Grid기준): {cellPos}");

            CellType type = mapData.GetCellByTilemapCoord(cellPos.x, cellPos.y);

            Debug.Log($"타입: {type}");

            if (type == CellType.Buildable)
            {
                Debug.Log("타워 설치 가능");
            }
            else
            {
                Debug.Log("타워 설치 불가");
            }
        }
    }
}
