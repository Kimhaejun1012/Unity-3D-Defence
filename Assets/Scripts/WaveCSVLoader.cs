using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class WaveCSVLoader
{
    public static List<WaveRow> Load(string csvName)
    {
        TextAsset csv = Resources.Load<TextAsset>(csvName);
        if (csv == null)
        {
            Debug.LogError($"CSV '{csvName}' not found");
            return null;
        }

        List<WaveRow> rows = new List<WaveRow>();
        string[] lines = csv.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] cols = lines[i].Trim().Split(',');

            WaveRow row = new WaveRow
            {
                wave = int.Parse(cols[0]),
                monsterType = cols[1],
                count = int.Parse(cols[2]),
                interval = float.Parse(cols[3])
            };

            rows.Add(row);
        }

        return rows;
    }
}
