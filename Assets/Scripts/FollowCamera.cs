using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; //따라갈 대상 설정
    public float offsetX = -6f;

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
