using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private bool isDead = false;
    private Animator animator;

    public float widthPadding = 5f;  //오브젝트 사이의 폭
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
                animator.SetTrigger("isDie"); // "Die" 트리거 발동 (애니메이션 시작)
            }

            GameManager.Instance.AddScore(100);

            gameObject.GetComponent<BoxCollider2D>().enabled = false; //몬스터 충돌 해제
            Destroy(gameObject, 0.5f); // 0.5초 뒤에 파괴 (애니메이션 길이에 맞게 조절)
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        // X 방향으로만 이동 (Y는 현재 Y 유지)
        float newX = lastPosition.x + widthPadding + totalPadding;
        float currentY = transform.position.y; // 현재 Y 그대로 유지

        Vector3 placePosition = new Vector3(newX, currentY, transform.position.z);

        transform.position = placePosition;

        return placePosition;
    }



    private void Update() //화면 나갈시 파괴
    {
        if (transform.position.x < Camera.main.transform.position.x - 15f) // 왼쪽 화면 밖으로
        {
            Destroy(gameObject);
        }
    }
}

