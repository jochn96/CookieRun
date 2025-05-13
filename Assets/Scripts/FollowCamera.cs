using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; //���� ��� ����
    public float offsetX = -6f; //ī�޶� ���� ��ġ ����

    void Start()
    {
   
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;
        pos.x = target.position.x - offsetX;
        transform.position = pos;
    }
}
