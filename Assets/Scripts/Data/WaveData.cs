using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public int NumberOfEnemies = 3;
    public EnemyData EnemyType;
    public float CooldownBetweenEnemies = 1f;
}
