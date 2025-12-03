using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    private Monster target;
    private Transform targetPoint;
    private float speed;

    protected int damage;
    protected int level;

    public virtual void Init(Monster target, float speed,int damage, int level)
    {
        this.target = target;
        this.speed = speed;

        this.damage = damage;
        this.level = level;

        targetPoint = target.targetPoint;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            GetComponent<PooledObject>().Return();
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Monster monster))
        {
            OnHit(monster);
            GetComponent<PooledObject>().Return();
        }
    }

    protected abstract void OnHit(Monster monster);
}
