using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CSVLoader
{
    public static Dictionary<string, string> Load(string fileName, string lang)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        Dictionary<string, string> dict = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split('\n');

        string[] headers = lines[0].Split(',');
        for (int i = 0; i < headers.Length; i++)
            headers[i] = headers[i].Trim();

        int langIndex = System.Array.IndexOf(headers, lang);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] row = lines[i].Split(',');

            for (int k = 0; k < row.Length; k++)
                row[k] = row[k].Trim();

            string key = row[0];
            string value = row[langIndex];

            dict[key] = value;
        }

        return dict;
    }
}