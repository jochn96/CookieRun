using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private bool isDead = false;
    private Animator animator;

    public float widthPadding = 5f;  //������Ʈ ������ ��
    public float totalPadding = 12f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (isDead) return;

        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log(collision.gameObject.GetComponentInChildren<PlayerAttack>().isattack);
            if (collision.gameObject.GetComponentInChildren<PlayerAttack>().isattack == false)
            {
                return;
            }

            isDead = true;

            SoundManager.Instance.PlaySFX("MonsterDie");

            if (animator != null)
            {
                animator.SetTrigger("isDie"); // "Die" Ʈ���� �ߵ� (�ִϸ��̼� ����)
            }

            GameManager.Instance.AddScore(100);

            gameObject.GetComponent<BoxCollider2D>().enabled = false; //���� �浹 ����
            Destroy(gameObject, 0.5f); // 0.5�� �ڿ� �ı� (�ִϸ��̼� ���̿� �°� ����)
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        // X �������θ� �̵� (Y�� ���� Y ����)
        float newX = lastPosition.x + widthPadding + totalPadding;
        float currentY = transform.position.y; // ���� Y �״�� ����

        Vector3 placePosition = new Vector3(newX, currentY, transform.position.z);

        transform.position = placePosition;

        return placePosition;
    }



    private void Update() //ȭ�� ������ �ı�
    {
        if (transform.position.x < Camera.main.transform.position.x - 15f) // ���� ȭ�� ������
        {
            Destroy(gameObject);
        }
    }
}

