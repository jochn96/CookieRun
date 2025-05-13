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

    public Text scoreText;  //현재 점수
    public Text highScoreText; //최고 점수
    private float score = 0f;
    private float highScore = 0f;

    private bool isGameOver = false;


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

        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        UpdateScoreUI();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 10f;
            UpdateScoreUI();
        }
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

    private void UpdateScoreUI() //점수 반영
    {
        if (scoreText != null)
            scoreText.text = $"Score: {(int)score}";
        if (highScoreText != null)
            highScoreText.text = $"Best: {(int)highScore}";
    }

    public void GameOverScoreCheck() //게임종료시 점수확인
    {
        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }
}
