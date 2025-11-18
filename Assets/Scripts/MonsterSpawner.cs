using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ObjectPoolManager.Instance.Spawn("TestMonster", WayPointManager.Instance.waypoints[0].position, Quaternion.identity);
        }
    }
}