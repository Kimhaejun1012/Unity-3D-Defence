using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public string monsterKey;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var obj = ObjectPoolManager.Instance.Spawn(monsterKey, Vector3.zero, Quaternion.identity);
            var monster = obj.GetComponent<Monster>();

            monster.Init(WayPointManager.Instance.waypoints);
        }
    }
}