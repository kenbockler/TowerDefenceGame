using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/TowerData")]
public class TowerData : ScriptableObject
{
    public string Name;
    public int Cost;
    public string ShortCut;
    public float ShootingDelay;
    public Sprite Icon;
    public GameObject TowerPrefab;
    public Projectile ProjectilePrefab;
}
