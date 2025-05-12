using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float moveSpeed = 3f; //움직이는 속도
    public float jumpForce = 8f; //점프 속도
    public bool isDead = false;
    float deathCooldown = 0f;
    public float slideSpeed = 3f; //슬라이딩 속도

    bool isJump = false;
    bool isSlide = false;

    bool isGrounded = false; //땅 밟고있는지

    public bool godMode = false;

    public Transform groundCheck;   // 캐릭터 발밑 위치
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("애니메이터 없음");

        if (_rigidbody == null)
            Debug.LogError("리지드바디 없음");


        isJump = false;
        isSlide = false;




    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                //게임 재시작 
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }


        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isJump = true;
                


            }

            else if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                Debug.Log("작동됨?");
                isSlide = true;
            }

        }
        //animator.SetBool("isJump", !isGrounded);   땅이 아닐시 점프모션

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //바닥 감지 기능 추가
             
        
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;  // _rigidbody 가속도 가져옴
        velocity.x = moveSpeed;

        if (isJump)
        {
            velocity.y = jumpForce;
            isJump = false;
        }

        // 점프 애니메이션은 땅에 안 닿아 있으면 true
        animator.SetBool("isJump", !isGrounded);

        // 슬라이드 처리
        if (isSlide)
        {
            velocity.x = slideSpeed;
            animator.SetBool("isSlide", true);
            isSlide = false;
        }
        else
        {
            animator.SetBool("isSlide", false);
        }


        _rigidbody.velocity = velocity;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (godMode) return;

        if (isDead) return;




        animator.SetInteger("IsDead", 1);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }



}
