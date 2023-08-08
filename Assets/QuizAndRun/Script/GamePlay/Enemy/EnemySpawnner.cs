using System;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    [SerializeField] float minSpawnDist,maxSpawnDist;
    [SerializeField] EnemyController[] enemys;
    [SerializeField] float enemyPosY;
    private Vector3 lastEnemyPos;

    public EnemyController SpawnEnemy()
    {
        if (enemys.Length <= 0) return null;
        Vector3 spawnPos = Vector3.zero;
        EnemyController enemyController = null;
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] != null)
            {
                float dist = UnityEngine.Random.Range(minSpawnDist,maxSpawnDist);
                spawnPos = new Vector3(lastEnemyPos.x + dist , enemyPosY,0);
                int rand = UnityEngine.Random.Range(0,enemys.Length);
                EnemyController enemy = enemys[rand];
                enemyController = Instantiate(enemy, spawnPos, Quaternion.identity);
                lastEnemyPos = spawnPos;
            }
        }
        return enemyController;
    }
}
