using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D attackCollider; // <<<< 수정됨 : 공격 판정용 콜라이더
    public Player player;
    public bool isattack = false;

    void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            player = GetComponent<Player>();
        }
    }

    private void Start()
    {
        DisableAttackCollider();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //왼쪽 클릭 시
        {
            
            animator.SetTrigger("isAttack"); //애니메이션 트리거
            SoundManager.Instance.PlaySFX("Attack");
        }
       

       
    }

    // 애니메이션 이벤트로 호출될 함수
    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
        isattack = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = true;
        isattack = false;
    }

    public void playerDie()
    {
        player.deathUI.SetActive(true);
        GameManager.Instance.GameOverScoreCheck();
    }
}

