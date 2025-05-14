using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRun : MonoBehaviour
{
    [Header("이동 속도 설정")]
    public float runSpeed = 7f;               // 초기 속도
    public float speedIncreaseAmount = 3f;    // 증가량
    public float speedIncreaseInterval = 10f; // 증가 간격
    public float maxSpeed = 20f;              // 최대 속도

    private float nextSpeedIncreaseTime = 0f;

    private Rigidbody2D rb;
    private float moveInput = 1f; // 오른쪽 이동

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextSpeedIncreaseTime = Time.time + speedIncreaseInterval;
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
        // Rigidbody2D로 계속 이동
        rb.velocity = new Vector2(runSpeed * moveInput, rb.velocity.y);
        transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
        
    }
}

