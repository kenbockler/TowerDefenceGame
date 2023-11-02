using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Waypoint Waypoint;
    private float speed;
    private int health;
    private int damage;
    private int gold;
    private SpriteRenderer spriteRenderer;

    public void SetEnemyData(EnemyData enemyData)
    {
        health = enemyData.Health;
        damage = enemyData.Damage;
        gold = enemyData.Gold;
        speed = enemyData.MovementSpeed;

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null && enemyData.Sprite != null)
            spriteRenderer.sprite = enemyData.Sprite;
    }

    void Update()
    {
        if (Waypoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, Waypoint.transform.position, speed * Time.deltaTime);

        float distance = Vector3.SqrMagnitude(transform.position - Waypoint.transform.position);
        if (distance <= float.Epsilon)
        {
            Waypoint = Waypoint.GetNextWaypoint();
            if (Waypoint == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
