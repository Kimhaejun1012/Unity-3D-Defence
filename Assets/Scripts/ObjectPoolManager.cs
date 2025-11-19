using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [System.Serializable]
    public class PoolInfo
    {
        public string key;
        public GameObject prefab;
        public int initialSize = 10;
    }

    public List<PoolInfo> poolInfos = new List<PoolInfo>();

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(this);
        InitializePools();
    }
    private void InitializePools()
    {
        foreach (var info in poolInfos)
        {
            if (info.prefab == null)
            {
                Debug.LogError($"[PoolManager] Missing prefab for key: {info.key}");
                continue;
            }

            prefabCache[info.key] = info.prefab;
            pools[info.key] = new Queue<GameObject>();

            for (int i = 0; i < info.initialSize; i++)
            {
                GameObject obj = CreateNewObject(info.key);
                pools[info.key].Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }
    private GameObject CreateNewObject(string key)
    {
        GameObject prefab = prefabCache[key];
        GameObject obj = Instantiate(prefab);

        var returner = obj.GetComponent<PooledObject>();
        if (returner == null)
        {
            returner = obj.AddComponent<PooledObject>();
        }
        returner.poolKey = key;

        obj.transform.SetParent(transform);
        obj.SetActive(false);
        return obj;
    }

    public GameObject Spawn(string key, Vector3 pos, Quaternion rot)
    {
        if (!pools.ContainsKey(key))
        {
            Debug.LogError($"[PoolManager] No pool with key: {key}");
            return null;
        }

        GameObject obj;

        if (pools[key].Count > 0)
        {
            obj = pools[key].Dequeue();
        }
        else
        {
            obj = CreateNewObject(key);
        }

        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);
        return obj;
    }
    public void Return(string key, GameObject obj)
    {
        obj.SetActive(false);
        pools[key].Enqueue(obj);
    }
}
