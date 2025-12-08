using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static readonly string fileName = "save.json";

    public static SaveData data = new SaveData();

    private static string FilePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
    }

    public static void Load()
    {
        if (!File.Exists(FilePath))
        {
            Save();
            return;
        }

        string json = File.ReadAllText(FilePath);
        data = JsonUtility.FromJson<SaveData>(json);

    }
}
