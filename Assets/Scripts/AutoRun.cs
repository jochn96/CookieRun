using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRun : MonoBehaviour
{
    public float moveSpeed = 3f; // �̵��ӵ�

    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}

