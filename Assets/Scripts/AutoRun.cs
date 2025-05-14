using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRun : MonoBehaviour
{
    [Header("�̵� �ӵ� ����")]
    public float runSpeed = 7f;               // �ʱ� �ӵ�
    public float speedIncreaseAmount = 3f;    // ������
    public float speedIncreaseInterval = 10f; // ���� ����
    public float maxSpeed = 20f;              // �ִ� �ӵ�

    private float nextSpeedIncreaseTime = 0f;

    private Rigidbody2D rb;
    private float moveInput = 1f; // ������ �̵�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextSpeedIncreaseTime = Time.time + speedIncreaseInterval;
    }

    void Update()
    {
        // ���� �ð����� �ӵ� ����
        if (Time.time >= nextSpeedIncreaseTime)
        {
            
            runSpeed = Mathf.Min(runSpeed + speedIncreaseAmount, maxSpeed);
            nextSpeedIncreaseTime += speedIncreaseInterval;
            
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D�� ��� �̵�
        rb.velocity = new Vector2(runSpeed * moveInput, rb.velocity.y);
        transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
        
    }
}

