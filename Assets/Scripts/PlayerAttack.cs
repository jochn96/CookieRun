using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D attackCollider; // <<<< ������ : ���� ������ �ݶ��̴�
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
        if (Input.GetMouseButtonDown(0)) //���� Ŭ�� ��
        {
            
            animator.SetTrigger("isAttack"); //�ִϸ��̼� Ʈ����
            SoundManager.Instance.PlaySFX("Attack");
        }
       

       
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��� �Լ�
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

