using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 8f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("애니메이터 없음");

        if (_rigidbody == null)
            Debug.LogError("리지드바디 없음");


    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {

        }
        else
        {
            //if(Input.GetKeyDown(KeyCode.Space)) UI 만들고 추가 예정 
        }


    }
}
