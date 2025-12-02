using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Localization
{
    private static Dictionary<string, string> table;

    public static event Action OnLanguageChanged;

    public static void LoadLanguage(string lang)
    {
        table = CSVLoader.Load("localization", lang);
    }

    public static string Get(string key)
    {
        if (table.ContainsKey(key))
            return table[key];
        return null;
    }
    public static void SetLanguage(string lang)
    {
        SaveManager.data.language = lang;
        table = CSVLoader.Load("localization", lang);
        OnLanguageChanged?.Invoke();
        SaveManager.Save();
    }

    public static void ApplyLanguageToAll()
    {
        OnLanguageChanged?.Invoke();
    }
}
