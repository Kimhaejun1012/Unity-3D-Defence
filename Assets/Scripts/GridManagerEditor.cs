#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridManager gm = (GridManager)target;

        GUILayout.Space(10);

        if (GUILayout.Button("새 MapData 생성 & 베이크"))
        {
            CreateAndBake(gm);
        }

        if (GUILayout.Button("기존 MapData에 베이크"))
        {
            gm.BakeToMapData();
            EditorUtility.SetDirty(gm.mapData);
            AssetDatabase.SaveAssets();
        }
    }

    private void CreateAndBake(GridManager gm)
    {
        MapData newMap = ScriptableObject.CreateInstance<MapData>();
        gm.mapData = newMap;

        gm.BakeToMapData();

        string path = EditorUtility.SaveFilePanelInProject(
            "MapData 저장",
            "NewMapData",
            "asset",
            "저장할 위치를 선택하세요."
        );

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newMap, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("MapData.asset 생성 완료");
        }
        else
        {
            Debug.LogWarning("MapData 저장 취소됨");
        }
    }
}
#endif
