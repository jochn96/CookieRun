using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;

    public GameObject deathUI;

    public Text scoreText;  //현재 점수
    public Text highScoreText; //최고 점수
    public TMP_Text resultScoreText; // 결과창 현재점수
    public TMP_Text resultHighScoreText; // 결과창 최고점수
    private float score = 0f;
    private float highScore = 0f;
    
    private bool isGameOver = false;



    public float timeElapsed = 0f;
    public int difficultyLevel = 0;


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
        // 20초마다 난이도 증가
        if ((int)(timeElapsed / 20f) > difficultyLevel)
        {
            difficultyLevel++;
            Debug.Log("난이도 증가! 현재 레벨: " + difficultyLevel);
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

        if (resultScoreText != null) //결과창에 현재점수 표시
            resultScoreText.text = $"현재점수 : {(int)score}";
        if (resultHighScoreText != null) // 결과창에 최고점수 표시
            resultHighScoreText.text = $"최고점수 : {(int)highScore}";

        if (scoreText != null) //결과창 출력시 오른쪽 상단의 현재점수와 최고점수 UI 숨김
            scoreText.gameObject.SetActive(false);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(false);

        if (deathUI != null) // 게임오버 UI 표시
            deathUI.SetActive(true);
    
           
    }


}
