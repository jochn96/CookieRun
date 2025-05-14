using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;
    [Header("ÇÃ·¹ÀÌ¾î ¼³Á¤")]
    public float moveSpeed = 3f; //¿òÁ÷ÀÌ´Â ¼Óµµ
    public float jumpForce = 8f; //Á¡ÇÁ ¼Óµµ
    public bool isDead = false;
    public float slideSpeed = 3f; //½½¶óÀÌµù ¼Óµµ
    public float fallMultiplier = 3.5f; //Á¡ÇÁ ÇÏ°­½Ã ¹Ş´Â Áß·Â°ª

    public GameObject deathUI; //°ÔÀÓ¿À¹ö UI
       
    

    bool isJump = false;
    bool isSlide = false;

    bool isGrounded = false; //¶¥ ¹â°íÀÖ´ÂÁö

    public bool godMode = false;

    public Transform groundCheck;   // Ä³¸¯ÅÍ ¹ß¹Ø À§Ä¡
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Äİ¶óÀÌ´õ ¼³Á¤")]
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
            Debug.LogError("¾Ö´Ï¸ŞÀÌÅÍ ¾øÀ½");

        if (_rigidbody == null)
            Debug.LogError("¸®Áöµå¹Ùµğ ¾øÀ½");

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

            //animator.SetBool("isJump", !isGrounded);   ¶¥ÀÌ ¾Æ´Ò½Ã Á¡ÇÁ¸ğ¼Ç
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //¹Ù´Ú °¨Áö ±â´É Ãß°¡


        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;  // _rigidbody °¡¼Óµµ °¡Á®¿È
        velocity.x = moveSpeed;

        if (isJump)
        {
            velocity.y = jumpForce;
            isJump = false;
        }

        // Á¡ÇÁ ¾Ö´Ï¸ŞÀÌ¼ÇÀº ¶¥¿¡ ¾È ´ê¾Æ ÀÖÀ¸¸é true
        animator.SetBool("isJump", !isGrounded);

        // ½½¶óÀÌµå Ã³¸®
        if (isSlide)
        {

            velocity.x = slideSpeed;
            animator.SetBool("isSlide", true);            
        }
        else
        {
            animator.SetBool("isSlide", false);
        }

        if (_rigidbody.velocity.y < 0) // ³«ÇÏ ÁßÀÏ ¶§¸¸ Áß·Â¿µÇâ
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

        if (collision.gameObject.CompareTag("Obstacle")) //Ãæµ¹½Ã µ¥¹ÌÁö Ã³¸®
        {
            SoundManager.Instance.PlaySFX("Hit");
            animator.SetBool("isHit", true);

            GameManager.Instance.TakeDamage(15); // Ãæµ¹½Ã 15 µ¥¹ÌÁö
        }




        //animator.SetInteger("IsDead", 1); 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

        if (isDead) return;

        animator.SetBool("isHit", false);
    }

    public void RestartGame() //°ÔÀÓ Àç½ÃÀÛ ±â´É Ãß°¡
    {
        Time.timeScale = 1f; //½Ã°£ ´Ù½Ã ½Ã°£ Èå¸£°Ô
        GameManager.Instance.ResetGame(); //Á¡¼ö Ã¼·Â UI ¸®¼Â
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Die()
    {
        isDead = true;
        animator.SetInteger("IsDead", 1);
        deathUI.SetActive(true);
        GameManager.Instance.GameOverScoreCheck(); //°ÔÀÓ¸Å´ÏÀú Á¡¼öÈ£Ãâ
        Time.timeScale = 0f; // °ÔÀÓ Á¤Áö
        if (isSlide)
        {
            SetSlideCollider(false);
            isSlide = false;
        }


    }

<<<<<<< HEAD

    private void SetSlideCollider(bool isSlide)
    {
         
        if (isSlide)
        {
            // ½½¶óÀÌµù Å©±â·Î º¯°æ
            playerCollider.size = slideColliderSize;
            
        }
        else
        {
            // ¿ø·¡ Å©±â·Î º¹¿ø
            playerCollider.size = originalColliderSize;
            
        } 
    }
    


=======
   
>>>>>>> feature/ê¹€ì¤€í¬
}
