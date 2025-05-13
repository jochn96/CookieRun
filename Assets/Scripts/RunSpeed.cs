using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSpeed : MonoBehaviour
{
    public float runSpeed = 5f;               // 시작 속도
    public float speedIncreaseAmount = 1f;    // 증가할 속도 양
    public float speedIncreaseInterval = 20f; // 증가 간격 (초)
    public float maxSpeed = 15f;              // 최대 속도 제한

    private float nextSpeedIncreaseTime = 20f;

    private Rigidbody2D rb;
    private float moveInput = 1f; // 오른쪽으로 계속 달리기

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 일정 시간마다 속도 증가
        if (Time.time >= nextSpeedIncreaseTime)
        {
            runSpeed = Mathf.Min(runSpeed + speedIncreaseAmount, maxSpeed);
            nextSpeedIncreaseTime += speedIncreaseInterval;
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D를 사용해 오른쪽으로 달리기
        rb.velocity = new Vector2(runSpeed * moveInput, rb.velocity.y);
    }
}
