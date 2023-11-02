using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public WaypointFollower FollowerPrefab;
    private Waypoint waypoint;
    private ScenarioData currentScenario;
    private int currentWaveIndex = 0;

    private float nextSpawnTime = 0;
    private int spawnedEnemiesCount = 0;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        Events.OnStartLevel += OnStartLevel;
    }

    void OnDestroy()
    {
        Events.OnStartLevel -= OnStartLevel;
    }

    private void OnStartLevel(ScenarioData data)
    {
        currentScenario = data;
        currentWaveIndex = 0;
        PrepareNextWave();
    }

    void Update()
    {
        if (currentScenario != null && currentWaveIndex < currentScenario.Waves.Length)
        {
            WaveData currentWave = currentScenario.Waves[currentWaveIndex];

            if (spawnedEnemiesCount < currentWave.NumberOfEnemies)
            {
                if (Time.time > nextSpawnTime)
                {
                    SpawnEnemy(currentWave.EnemyType);
                    nextSpawnTime = Time.time + currentWave.CooldownBetweenEnemies;
                    spawnedEnemiesCount++;
                }
            }
            else
            {
                if (AreAllEnemiesDefeated())
                {
                    currentWaveIndex++;
                    if (currentWaveIndex < currentScenario.Waves.Length)
                    {
                        PrepareNextWave();
                    }
                    else
                    {
                        Debug.Log("Kõik lained on läbi. Mängu lõpp või järgmine tasand?");
                        // Siin võib olla kood taseme lõpetamiseks või järgmise tasandi alustamiseks.
                        Events.EndLevel(true);
                    }
                }
            }
        }
    }

    void SpawnEnemy(EnemyData enemyData)
    {
        WaypointFollower follower = Instantiate(FollowerPrefab, transform.position, Quaternion.identity);
        follower.Waypoint = waypoint;
        follower.SetEnemyData(enemyData);
    }

    void PrepareNextWave()
    {
        spawnedEnemiesCount = 0;
        nextSpawnTime = Time.time;
    }

    bool AreAllEnemiesDefeated()
    {
        return FindObjectsOfType<WaypointFollower>().Length == 0;
    }
}
