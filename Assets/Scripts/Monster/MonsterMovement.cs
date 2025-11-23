using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private float speed;
    private int index;
    private Transform[] waypoints;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public void Init(float moveSpeed, Transform[] transforms)
    {
        speed = moveSpeed;
        index = 0;

        waypoints = transforms;
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        if (waypoints == null || index >= waypoints.Length) return;

        Vector3 target = waypoints[index].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.transform.LookAt(target);
        if (Vector3.Distance(transform.position, target) < 0.1f)
            index++;

        if (index >= waypoints.Length)
        {
            GetComponent<PooledObject>().Return();
        }
    }
}
