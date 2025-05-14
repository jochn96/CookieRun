using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    [Header("�÷��̾� ����")]
    public float moveSpeed = 3f; //�����̴� �ӵ�
    public float jumpForce = 8f; //���� �ӵ�
    public bool isDead = false;
    public float slideSpeed = 3f; //�����̵� �ӵ�
    public float fallMultiplier = 3.5f; //���� �ϰ��� �޴� �߷°�

    public GameObject deathUI; //���ӿ��� UI
       
    

    bool isJump = false;
    bool isSlide = false;

    bool isGrounded = false; //�� ����ִ���

    public bool godMode = false;

    public Transform groundCheck;   // ĳ���� �߹� ��ġ
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("�ݶ��̴� ����")]
    private BoxCollider2D playerCollider;
    private Vector2 originalColliderSize;
    private Vector2 slideColliderSize;
    public float slideColliderheight = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        if (animator == null)
            Debug.LogError("�ִϸ����� ����");

        if (_rigidbody == null)
            Debug.LogError("������ٵ� ����");

        originalColliderSize = playerCollider.size;
        slideColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y * slideColliderheight);

        isJump = false;
        isSlide = false;




    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                SoundManager.Instance.PlaySFX("Jump");
                isJump = true;
            }
            else if (Input.GetKey(KeyCode.LeftShift))       
            {
                SetSlideCollider(true);
                isSlide = true;
            }
            else
            {
                SetSlideCollider(false);
                isSlide = false;
            }

            //animator.SetBool("isJump", !isGrounded);   ���� �ƴҽ� �������
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
        }
        else
        {
            animator.SetBool("isSlide", false);
        }

        if (_rigidbody.velocity.y < 0) // ���� ���� ���� �߷¿���
        {
            velocity.y += Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }


        _rigidbody.velocity = velocity;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;


        if (godMode) return;

        if (isDead) return;

        if (collision.gameObject.CompareTag("Obstacle")) //�浹�� ������ ó��
        {
            SoundManager.Instance.PlaySFX("Hit");
            animator.SetBool("isHit", true);

            GameManager.Instance.TakeDamage(15); // �浹�� 15 ������
        }

        else if (collision.gameObject.CompareTag("Monster"))
        {
            SoundManager.Instance.PlaySFX("Hit");
            animator.SetBool("isHit", true);

            GameManager.Instance.TakeDamage(15); // �浹�� 20 ������
        }




        //animator.SetInteger("IsDead", 1); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        if (isDead) return;

        animator.SetBool("isHit", false);
    }

    public void RestartGame() //���� ����� ��� �߰�
    {
        Time.timeScale = 1f; //�ð� �ٽ� �ð� �帣��
        GameManager.Instance.ResetGame(); //���� ü�� UI ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Die()
    {
        isDead = true;
        animator.SetInteger("IsDead", 1);
        deathUI.SetActive(true);
        GameManager.Instance.GameOverScoreCheck(); //���ӸŴ��� ����ȣ��
        Time.timeScale = 0f; // ���� ����
        if (isSlide)
        {
            SetSlideCollider(false);
            isSlide = false;
        }


    }


    private void SetSlideCollider(bool isSlide)
    {
         
        if (isSlide)
        {
            // �����̵� ũ��� ����
            playerCollider.size = slideColliderSize;
            
        }
        else
        {
            // ���� ũ��� ����
            playerCollider.size = originalColliderSize;
            
        } 
    }
    


}
