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
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); //시작 시 체력바 초기화
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //음수방지
        Debug.Log("플레이어 체력: " + currentHealth);

        UpdateHealthBar(); //체력바 갱신

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
        UpdateHealthBar(); //체력 리셋 시 UI 반영
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
