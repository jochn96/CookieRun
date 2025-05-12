using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float moveSpeed = 3f; //�����̴� �ӵ�
    public float jumpForce = 8f; //���� �ӵ�
    public bool isDead = false;
    float deathCooldown = 0f;
    public float slideSpeed = 3f; //�����̵� �ӵ�

    bool isJump = false;
    bool isSlide = false;

    bool isGrounded = false; //�� ����ִ���

    public bool godMode = false;

    public Transform groundCheck;   // ĳ���� �߹� ��ġ
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("�ִϸ����� ����");

        if (_rigidbody == null)
            Debug.LogError("������ٵ� ����");


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
                //���� ����� 
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
                Debug.Log("�۵���?");
                isSlide = true;
            }

        }
        //animator.SetBool("isJump", !isGrounded);   ���� �ƴҽ� �������

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //�ٴ� ���� ��� �߰�
             
        
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;  // _rigidbody ���ӵ� ������
        velocity.x = moveSpeed;

        if (isJump)
        {
            velocity.y = jumpForce;
            isJump = false;
        }

        // ���� �ִϸ��̼��� ���� �� ��� ������ true
        animator.SetBool("isJump", !isGrounded);

        // �����̵� ó��
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
