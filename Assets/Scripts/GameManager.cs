using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int maxHealth = 100;
    public int currentHealth;


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
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("플레이어 체력: " + currentHealth);

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
    }
}
