using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float moveSpeed = 3f; //�����̴� �ӵ�
    public float jumpForce = 8f; //���� �ӵ�
    public bool isDead = false;
    public float slideSpeed = 3f; //�����̵� �ӵ�
    public GameObject deathUI;

    //float deathCooldown = 0f; ���� ��ٿ��� �ʿ� ������ ����  �ּ�ó��


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
            //if (deathCooldown <= 0)
            //{
            //    //���� �����
            //}
            //else
            //{
            //    deathCooldown -= Time.deltaTime;
            //}
            // �� �κе� UI �˾����� ����Ǿ� �ʿ���� �κ��̶� �ּ�ó��.

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isJump = true;



            }

            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("�۵���?");
                isSlide = true;
            }
            //animator.SetBool("isJump", !isGrounded);   ���� �ƴҽ� �������

            if (Input.GetKeyDown(KeyCode.K) && !isDead)    //�׽�Ʈ������ ����ϴ� �ڵ� �־�����ϴ�.
            {                                              //�ʿ� ������ ������ �����մϴ�!
                isDead = true;
                animator.SetInteger("IsDead", 1);
                deathUI.SetActive(true);
                Debug.Log("�׽�Ʈ������ ĳ���� ��� ó����");
            }
        }
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




        //animator.SetInteger("IsDead", 1); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        if (isDead) return;

        isDead = true;
        animator.SetInteger("IsDead", 1);

        if (deathUI != null)
        {
            deathUI.SetActive(true); //���� ������ UI ȭ�� ��� �߰�
        }
    }

    public void RestartGame() //���� ����� ��� �߰�
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
