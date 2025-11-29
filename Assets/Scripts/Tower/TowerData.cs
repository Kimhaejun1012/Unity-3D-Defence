using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "DF/Tower Data")]
public class TowerData : ScriptableObject
{
    public int maxLevel;
    public float[] range;
    public float[] attackSpeed;
    public int[] baseDamage;
}
