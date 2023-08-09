using System;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    [SerializeField] float minSpawnDist,maxSpawnDist;
    [SerializeField] EnemyController[] enemys;
    private Vector3 lastEnemyPos;

    public EnemyController SpawnEnemy()
    {
        if (enemys.Length <= 0) return null;
        Vector3 spawnPos = Vector3.zero;
        EnemyController enemyController = null;

            if (enemys != null)
            {
                float dist = UnityEngine.Random.Range(minSpawnDist,maxSpawnDist);
                
                int rand = UnityEngine.Random.Range(0,enemys.Length);
                EnemyController enemy = enemys[rand];
                spawnPos = new Vector3(lastEnemyPos.x + dist, enemy.transform.position.y, 0);
                enemyController = Instantiate(enemy, spawnPos, Quaternion.identity);
                lastEnemyPos = spawnPos;
            }

        
        return enemyController;
    }
}
