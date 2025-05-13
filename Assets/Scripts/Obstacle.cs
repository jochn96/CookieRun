using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;         // 생성할 Obstacle 프리팹(Prefab)
    public Transform spawnPoint;              // Obstacle 생성 위치 기준
    public float spawnInterval = 5f;          // Obstacle 생성 간격

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacles();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnObstacles()
    {
        int level = GameManager.Instance.difficultyLevel;
        int obstacleCount = 1 + level;

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 spawnPos = spawnPoint.position + new Vector3(i * 2.0f, 0, 0);
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}
