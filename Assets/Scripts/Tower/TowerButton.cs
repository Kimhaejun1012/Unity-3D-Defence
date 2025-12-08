using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string towerKey;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowTowerInfo(towerKey);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideTowerInfo();
    }
}
