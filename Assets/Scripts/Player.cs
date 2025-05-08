using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float moveSpeed = 3f;
    public float jumpForce = 8f;
    public bool isDead = false;
    float deathCooldown = 0f;
    public float slideSpeed = 3f;

    bool isJump = false;
    bool isSlide = false;

    public bool godMode = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("�ִϸ����� ����");

        if (_rigidbody == null)
            Debug.LogError("������ٵ� ����");


    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {

        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }

            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {  
                isSlide = true;
            }    
                       
        }


    }

    private void FixedUpdate()
    {
        if(isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = moveSpeed;

        if (isJump)
        {
            velocity.y = jumpForce;
            isJump = false;
        }
        else if (isSlide)
        {
            velocity.x = slideSpeed;
            isSlide = false;
        }

        _rigidbody.velocity = velocity;

    }

}
