using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationText : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        Localization.OnLanguageChanged += UpdateText;
    }

    void Start()
    {
        UpdateText();
    }

    void OnDestroy()
    {
        Localization.OnLanguageChanged -= UpdateText;
    }

    public void UpdateText()
    {
        text.text = Localization.Get(key);
    }
}