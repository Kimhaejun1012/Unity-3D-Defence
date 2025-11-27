using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public string monsterKey;
    public Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = WayPointManager.Instance.waypoints[0].position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var obj = ObjectPoolManager.Instance.Spawn(monsterKey, spawnPosition, Quaternion.identity);
            var monster = obj.GetComponent<Monster>();

            monster.Init(WayPointManager.Instance.waypoints);
        }
    }
    public void Spawn(string key)
    {
        var obj = ObjectPoolManager.Instance.Spawn(key, spawnPosition, Quaternion.identity);
        var monster = obj.GetComponent<Monster>();

        monster.Init(WayPointManager.Instance.waypoints);
    }
}