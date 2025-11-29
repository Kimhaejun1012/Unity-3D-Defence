using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StandardAttackModule : MonoBehaviour, IAttackModule
{
    public void Execute(Monster target)
    {
        target.GetComponent<IDamageable>().TakeDamage(0);
    }
}
