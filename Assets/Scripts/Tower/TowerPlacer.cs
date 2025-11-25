using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;
    public Grid grid;

    public MapData originalMapData;
    private MapData mapData;
    [System.Serializable]
    public class TowerInfo
    {
        public GameObject towerPrefab;
        public GameObject previewPrefab;
        public int price;
    }

    public Dictionary<string, TowerInfo> towerDict = new Dictionary<string, TowerInfo>();

    public List<string> towerKeys;
    public List<TowerInfo> towerValues;

    private GameObject previewInstance;
    private bool isPlacing = false;

    private GameObject currentTowerPrefab;
    private GameObject currentPreviewPrefab;
    private int currentTowerPrice;

    void Awake()
    {
        mapData = Instantiate(originalMapData);

        if (grid == null && tilemap != null)
            grid = tilemap.layoutGrid;

        for (int i = 0; i < towerKeys.Count; i++)
        {
            towerDict[towerKeys[i]] = towerValues[i];
        }
    }

    void Update()
    {
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
    public void SelectTower(string key)
    {
        if (!towerDict.ContainsKey(key))
        {
            Debug.Log($"Tower Key '{key}' X ");
            return;
        }

        TowerInfo info = towerDict[key];

        if(PlayerStatsManager.Instance.CheckGold(info.price))
        {
            StartPlacing(info.towerPrefab, info.previewPrefab);
            currentTowerPrice = info.price;
        }
        else
        {
            Debug.Log("Not enough gold!");
            return;
        }
    }

    void StartPlacing(GameObject towerPrefab, GameObject previewPrefab)
    {
        if (previewInstance != null)
            Destroy(previewInstance);

        currentTowerPrefab = towerPrefab;
        currentPreviewPrefab = previewPrefab;

        previewInstance = Instantiate(currentPreviewPrefab);
        previewInstance.SetActive(true);

        TowerPreview preview = previewInstance.GetComponent<TowerPreview>();

        preview.rangeVisualizer.SetActive(true);

        float towerRange = currentTowerPrefab.GetComponent<TowerBase>().range;

        float scale = towerRange * 2f;
        preview.rangeVisualizer.transform.localScale =
            new Vector3(scale, 0.01f, scale);

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

        Instantiate(currentTowerPrefab, pos, Quaternion.identity);
        PlayerStatsManager.Instance.SpendGold(currentTowerPrice);

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
