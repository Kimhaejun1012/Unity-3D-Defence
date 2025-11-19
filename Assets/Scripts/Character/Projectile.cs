using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Monster target;
    private float speed;
    private string poolKey = "TestProjectile";

    public void Init(Monster target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    private void Update()
    {
        if (target == null)
        {
            ObjectPoolManager.Instance.Return(poolKey, gameObject);
            return;
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.TakeDamage(10);
            ObjectPoolManager.Instance.Return(poolKey, gameObject);
        }
    }
}
