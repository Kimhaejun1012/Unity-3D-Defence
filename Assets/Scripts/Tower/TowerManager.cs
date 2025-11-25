using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public Camera cam;
    public LayerMask towerLayerMask;

    private TowerBase currentTower;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, towerLayerMask))
            {
                TowerBase tower = hit.collider.GetComponent<TowerBase>();
                SelectTower(tower);
            }
            else
            {
                DeselectTower();
            }
        }
    }
    void SelectTower(TowerBase tower)
    {
        if (currentTower != null)
            currentTower.ShowRange(false);

        currentTower = tower;
        currentTower.ShowRange(true);
    }
    void DeselectTower()
    {
        if (currentTower != null)
        {
            currentTower.ShowRange(false);
            currentTower = null;
        }
    }
}
