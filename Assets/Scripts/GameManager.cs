using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); //���� �� ü�¹� �ʱ�ȭ
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //��������
        Debug.Log("�÷��̾� ü��: " + currentHealth);

        UpdateHealthBar(); //ü�¹� ����

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); //ü�� ���� �� UI �ݿ�
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

}
