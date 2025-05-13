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

    public Text scoreText;  //���� ����
    public Text highScoreText; //�ְ� ����
    public TMP_Text resultScoreText; // ���â ��������
    public TMP_Text resultHighScoreText; // ���â �ְ�����
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
        // 20�ʸ��� ���̵� ����
        if ((int)(timeElapsed / 20f) > difficultyLevel)
        {
            difficultyLevel++;
            Debug.Log("���̵� ����! ���� ����: " + difficultyLevel);
        }
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

    private void UpdateScoreUI() //���� �ݿ�
    {
        if (scoreText != null)
            scoreText.text = $"Score: {(int)score}";
        if (highScoreText != null)
            highScoreText.text = $"Best: {(int)highScore}";
    }

    public void GameOverScoreCheck() //��������� ����Ȯ��
    {
        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();

        if (resultScoreText != null) //���â�� �������� ǥ��
            resultScoreText.text = $"�������� : {(int)score}";
        if (resultHighScoreText != null) // ���â�� �ְ����� ǥ��
            resultHighScoreText.text = $"�ְ����� : {(int)highScore}";

        if (scoreText != null) //���â ��½� ������ ����� ���������� �ְ����� UI ����
            scoreText.gameObject.SetActive(false);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(false);

        if (deathUI != null) // ���ӿ��� UI ǥ��
            deathUI.SetActive(true);
    
           
    }


}
