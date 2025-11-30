using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    [Header("UI References")]
    public Image healthFill;
    public Transform statusContainer;

    private MonsterHealth health;
    private Camera cam;

    public Dictionary<string, GameObject> statusIcons;

    [Header("Status Icons")]
    public GameObject slowIcon;
    public GameObject stunIcon;

    private void Awake()
    {
        cam = Camera.main;
        health = GetComponentInParent<MonsterHealth>();

        statusIcons = new Dictionary<string, GameObject>()
        {
            { "Slow", slowIcon },
            { "Stun", stunIcon },
        };

        health.OnHealthChanged += UpdateHealthBar;
        health.OnDie += HideUI;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }

    private void UpdateHealthBar(float normalized)
    {
        healthFill.fillAmount = normalized;
    }

    private void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowStatus(string key)
    {
        if (statusIcons.ContainsKey(key))
            statusIcons[key].SetActive(true);
    }

    public void HideStatus(string key)
    {
        if (statusIcons.ContainsKey(key))
            statusIcons[key].SetActive(false);
    }
}
