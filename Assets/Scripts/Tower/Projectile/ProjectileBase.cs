using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    private Monster target;
    private float speed;

    public void Init(Monster target, float speed)
    {
        this.target = target;
        this.speed = speed;
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
            target.transform.position,
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