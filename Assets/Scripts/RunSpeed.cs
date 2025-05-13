using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSpeed : MonoBehaviour
{
    public float runSpeed = 5f;               // ���� �ӵ�
    public float speedIncreaseAmount = 1f;    // ������ �ӵ� ��
    public float speedIncreaseInterval = 20f; // ���� ���� (��)
    public float maxSpeed = 15f;              // �ִ� �ӵ� ����

    private float nextSpeedIncreaseTime = 20f;

    private Rigidbody2D rb;
    private float moveInput = 1f; // ���������� ��� �޸���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // Rigidbody2D�� ����� ���������� �޸���
        rb.velocity = new Vector2(runSpeed * moveInput, rb.velocity.y);
    }
}
