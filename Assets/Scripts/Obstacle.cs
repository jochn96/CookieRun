using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;         // ������ Obstacle ������(Prefab)
    public Transform spawnPoint;              // Obstacle ���� ��ġ ����
    public float spawnInterval = 5f;          // Obstacle ���� ����

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
