using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TowerAnimation : MonoBehaviour
{
    private Animator anim;
    private TowerBase tower;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        tower = GetComponent<TowerBase>();
    }
    private void Start()
    {
        anim.SetFloat("FireFloat", 1f * tower.data.attackSpeed[tower.level]);
    }

    private void OnEnable()
    {
        tower.OnFire += PlayFireAnim;
    }

    private void OnDisable()
    {
        tower.OnFire -= PlayFireAnim;
    }

    private void PlayFireAnim()
    {
        anim.SetTrigger("Fire");
    }
}
