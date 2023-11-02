using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TowerDefence/Enemy")]
public class EnemyData : ScriptableObject
{
    public int Health = 1;
    public int Damage = 1;
    public int Gold = 2;
    public float MovementSpeed = 2f;
    public Sprite Sprite;
}
