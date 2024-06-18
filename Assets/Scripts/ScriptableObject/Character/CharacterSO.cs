using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterData", menuName ="CharacterData/CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    public Define.CharacterType characterType;

    [Header("Default")]
    public int chaId;
    public string displayName;

    [Header("Status")]
    public float maxHp;
    public float currentHp;

    [Header("Movement")]
    public float moveSpeed;
    public float rotSpeed;

    [Header("Patrol")]
    public float patrolRadius;
    public LayerMask target;

    [Header("Attack")]
    public float damage;
}
