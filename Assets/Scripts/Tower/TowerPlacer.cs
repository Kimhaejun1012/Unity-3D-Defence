using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;

    public Grid grid;
    public GameObject towerPrefab;
    public GameObject towerPreviewPrefab;
    private GameObject previewInstance;
    private bool isPlacing = false;

    public MapData originalMapData;
    private MapData mapData;

    void Awake()
    {
        mapData = Instantiate(originalMapData);
        if (grid == null && tilemap != null)
            grid = tilemap.layoutGrid;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartPlacing();
        }

        if (isPlacing)
        {
            UpdatePreview();

            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacing();
            }
        }
    }

    void UpdatePreview()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(grid.transform.up, grid.transform.position);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 worldPos = ray.GetPoint(enter);
            Vector3Int cellPos = grid.WorldToCell(worldPos);
            Vector3 cellCenter = grid.GetCellCenterWorld(cellPos);

            previewInstance.transform.position = cellCenter;

            CellType type = mapData.GetCellByTilemapCoord(cellPos.x, cellPos.y);
            bool canPlace = type == CellType.Buildable;

            if (canPlace)
                SetPreviewColor(Color.green);
            else
                SetPreviewColor(Color.red);

            if (canPlace && Input.GetMouseButtonDown(0))
            {
                PlaceTower(cellCenter, cellPos);
            }
        }
    }
    void StartPlacing()
    {
        if (previewInstance != null)
            Destroy(previewInstance);

        previewInstance = Instantiate(towerPreviewPrefab);
        previewInstance.SetActive(true);

        SetPreviewColor(Color.red);
        isPlacing = true;
    }

    void SetPreviewColor(Color color)
    {
        Color c = color;
        c.a = 0.5f;

        foreach (var rend in previewInstance.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            rend.material.color = c;
        }
    }
    void PlaceTower(Vector3 pos, Vector3Int cellPos)
    {
        Instantiate(towerPrefab, pos, Quaternion.identity);

        mapData.SetCell(cellPos.x - mapData.originX, cellPos.y - mapData.originY, CellType.Blocked);

        if (previewInstance != null)
            Destroy(previewInstance);

        previewInstance = null;
        isPlacing = false;
    }

    void CancelPlacing()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
        isPlacing = false;
    }

}
