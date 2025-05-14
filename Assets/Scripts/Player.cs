using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    [Header("플레이어 설정")]
    public float moveSpeed = 3f; //움직이는 속도
    public float jumpForce = 8f; //점프 속도
    public bool isDead = false;
    public float slideSpeed = 3f; //슬라이딩 속도
    public float fallMultiplier = 3.5f; //점프 하강시 받는 중력값

    public GameObject deathUI; //게임오버 UI
       
    

    bool isJump = false;
    bool isSlide = false;

    bool isGrounded = false; //땅 밟고있는지

    public bool godMode = false;

    public Transform groundCheck;   // 캐릭터 발밑 위치
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("콜라이더 설정")]
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
            Debug.LogError("애니메이터 없음");

        if (_rigidbody == null)
            Debug.LogError("리지드바디 없음");

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

            //animator.SetBool("isJump", !isGrounded);   땅이 아닐시 점프모션
        }
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
        }
        else
        {
            animator.SetBool("isSlide", false);
        }

        if (_rigidbody.velocity.y < 0) // 낙하 중일 때만 중력영향
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

        if (collision.gameObject.CompareTag("Obstacle")) //충돌시 데미지 처리
        {
            SoundManager.Instance.PlaySFX("Hit");
            animator.SetBool("isHit", true);

            GameManager.Instance.TakeDamage(15); // 충돌시 15 데미지
        }

        else if (collision.gameObject.CompareTag("Monster"))
        {
            SoundManager.Instance.PlaySFX("Hit");
            animator.SetBool("isHit", true);

            GameManager.Instance.TakeDamage(15); // 충돌시 20 데미지
        }




        //animator.SetInteger("IsDead", 1); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        if (isDead) return;

        animator.SetBool("isHit", false);
    }

    public void RestartGame() //게임 재시작 기능 추가
    {
        Time.timeScale = 1f; //시간 다시 시간 흐르게
        GameManager.Instance.ResetGame(); //점수 체력 UI 리셋
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Die()
    {
        isDead = true;
        animator.SetInteger("IsDead", 1);
        deathUI.SetActive(true);
        GameManager.Instance.GameOverScoreCheck(); //게임매니저 점수호출
        Time.timeScale = 0f; // 게임 정지
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
            // 슬라이딩 크기로 변경
            playerCollider.size = slideColliderSize;
            
        }
        else
        {
            // 원래 크기로 복원
            playerCollider.size = originalColliderSize;
            
        } 
    }
    


}
