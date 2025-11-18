using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DF/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int maxHP;
    public float moveSpeed;
    public int reward;
}