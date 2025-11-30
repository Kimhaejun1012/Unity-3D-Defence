using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Localization
{
    private static Dictionary<string, string> table;

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
}
