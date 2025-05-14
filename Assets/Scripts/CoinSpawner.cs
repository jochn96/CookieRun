using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab; //코인 프리팹 연결
    public int coinCount = 10;    //한 번에 몇 개 생성할지
    public float spacing = 1.0f;  //코인 간 간격
    public float spawnInterval = 2f; //몇 초마다 생성할지
    public float spawnXOffset = 10f; //플레이어 기준 얼마나 앞에 생성할지

    public Transform player; // 플레이어 위치 추적용

    public LayerMask ObstacleLayer; // 장애물 프리팹 연결

    private void Start()
    {
        StartCoroutine(SpawnCoinLineRoutine());
    }

    IEnumerator SpawnCoinLineRoutine()
    {
        while (true)
        {
            SpawnCoinLine();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoinLine()
    {
        float fixedY = -2f;
        Vector3 startPos = new Vector3(player.position.x + spawnXOffset, fixedY, 0f);

        for (int i = 0; i < coinCount; i++)
        {
            Vector3 pos = startPos + new Vector3(i * spacing, 0f, 0f);

            // 장애물이 있는지 확인
            Collider2D hit = Physics2D.OverlapCircle(pos, 0.2f, ObstacleLayer);
            if (hit == null)
            {
                Instantiate(CoinPrefab, pos, Quaternion.identity);
            }
            else
            {
                // Debug.Log("장애물 때문에 코인 생략됨: " + hit.name);
            }
        }
    }

}
