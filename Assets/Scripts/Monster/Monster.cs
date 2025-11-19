using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;

    public Transform[] waypoints;
    private int waypointIndex = 0;

    void OnEnable()
    {

    }
    public void Init(Transform[] waypoints)
    {
        this.waypoints = waypoints;
        transform.position = waypoints[0].position;
        waypointIndex = 0;
    }
    void Update()
    {
        MoveAlongPath();
    }
    private void MoveAlongPath()
    {
        if (waypoints == null || waypointIndex >= waypoints.Length) return;

        Vector3 target = waypoints[waypointIndex].position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            monsterData.moveSpeed * Time.deltaTime
        );
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
            {
                gameObject.SetActive(false);
                GetComponent<PooledObject>().Die();
            }
        }
    }
}
