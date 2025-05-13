using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Obstacle : MonoBehaviour
{
    public float highPosY = 0f;
    public float lowPosY = 0f;



    public Transform topObject;
    public Transform bottomObject1;
    public Transform bottomObject2;


    public float widthPadding = 25f;  //������Ʈ ������ ��

    public float totalPadding = 65f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        // X �������θ� �̵� (Y�� ���� Y ����)
        float newX = lastPosition.x + widthPadding + totalPadding;
        float currentY = transform.position.y; // ���� Y �״�� ����

        Vector3 placePosition = new Vector3(newX, currentY, transform.position.z);

        transform.position = placePosition;

        return placePosition;
    }




        //Vector3 placePosition = lastPosition + new Vector3(widthPadding + totalPadding, 0);


        //transform.position = placePosition;

        //return placePosition;



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
}

    




