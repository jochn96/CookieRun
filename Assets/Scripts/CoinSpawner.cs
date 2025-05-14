using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab; //���� ������ ����
    public int coinCount = 10;    //�� ���� �� �� ��������
    public float spacing = 1.0f;  //���� �� ����
    public float spawnInterval = 2f; //�� �ʸ��� ��������
    public float spawnXOffset = 10f; //�÷��̾� ���� �󸶳� �տ� ��������

    public Transform player; // �÷��̾� ��ġ ������

    public LayerMask ObstacleLayer; // ��ֹ� ������ ����

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

            // ��ֹ��� �ִ��� Ȯ��
            Collider2D hit = Physics2D.OverlapCircle(pos, 0.2f, ObstacleLayer);
            if (hit == null)
            {
                Instantiate(CoinPrefab, pos, Quaternion.identity);
            }
            else
            {
                // Debug.Log("��ֹ� ������ ���� ������: " + hit.name);
            }
        }
    }

}
