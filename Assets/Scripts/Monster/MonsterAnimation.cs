using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    private Animator anim;
    private MonsterMovement movement;
    private MonsterHealth health;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<MonsterMovement>();
        health = GetComponent<MonsterHealth>();
    }

    private void OnEnable()
    {
        health.OnDie += PlayDie;
    }

    private void OnDisable()
    {
        health.OnDie -= PlayDie;
    }

    private void Update()
    {
        anim.SetFloat("Speed", movement.Speed);
    }

    private void PlayDie()
    {
        anim.speed = 1;
        anim.SetTrigger("Die");
    }
    public void OnDieAnimationComplete()
    {
        GetComponent<PooledObject>().Return();
    }
}
