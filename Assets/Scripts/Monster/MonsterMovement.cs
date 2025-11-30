using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private float speed;
    private float baseSpeed;
    private int index;
    private float currentSlowPercent;
    private float slowTimer;
    private Transform[] waypoints;

    public MonsterUI monsterUI;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    private void Awake()
    {
    }
    private void Start()
    {
        baseSpeed = Speed;
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
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
            {
                monsterUI.HideStatus("Slow");
                currentSlowPercent = 0f;
                Speed = baseSpeed;
            }
        }
        Move();
    }
    private void Move()
    {
        if (waypoints == null || index >= waypoints.Length || GetComponent<MonsterHealth>().isDie) return;

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
    public void ApplySlow(float percent, float duration)
    {
        if (percent < currentSlowPercent) return;

        monsterUI.ShowStatus("Slow");
        currentSlowPercent = percent;
        slowTimer = duration;

        Speed = baseSpeed * (1f - percent);
    }
    public void ApplyStun(float duration)
    {
        StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float t)
    {
        speed = 0f;
        GetComponent<Animator>().speed = 0f;
        monsterUI.ShowStatus("Stun");
        yield return new WaitForSeconds(t);
        GetComponent<Animator>().speed = 1f;
        monsterUI.HideStatus("Stun");
        Speed = baseSpeed;
    }
}
