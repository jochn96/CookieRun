using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timeElapsed = 0f;
    public int difficultyLevel = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // 20�ʸ��� ���̵� ����
        if ((int)(timeElapsed / 20f) > difficultyLevel)
        {
            difficultyLevel++;
            Debug.Log("���̵� ����! ���� ����: " + difficultyLevel);
        }
    }
}
