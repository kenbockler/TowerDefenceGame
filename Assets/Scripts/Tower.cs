using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData TowerData;
    private List<Health> EnemiesInRange;

    private float nextAttackTime;

    private void Awake()
    {
        EnemiesInRange = new List<Health>();
    }

    void Update()
    {
        if (Time.time > nextAttackTime && EnemiesInRange.Count > 0)
        {
            Attack();
            nextAttackTime = Time.time + TowerData.ShootingDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            EnemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            EnemiesInRange.Remove(enemy);
        }
    }

    void Attack()
    {
        // Ründame esimest vaenlast nimekirjas
        Health enemy = EnemiesInRange[0];
        Fire(enemy);
    }

    private void Fire(Health target)
    {
        // Instantsime projektiili ja seame selle sihtmärgi ja muud omadused
        Projectile projectile = GameObject.Instantiate(TowerData.ProjectilePrefab, transform.position, Quaternion.identity);

        if (projectile != null)
        {
            projectile.target = target.transform;
        }
        else
        {
            Debug.LogError("Projectile komponenti ei leitud!");
        }
    }

}
