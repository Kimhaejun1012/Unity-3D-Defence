using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private Animator anim;
    private TowerBase tower;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        tower = GetComponent<TowerBase>();
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
