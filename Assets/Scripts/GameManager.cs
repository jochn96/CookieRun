using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //�ν��Ͻ��� ������ ������ ã��
                _instance = FindObjectOfType<GameManager>();

                //������ ������ ���� ����
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }


    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;

    public GameObject deathUI;
    public GameObject gameGuideUI;

    public Text scoreText;  //���� ���� �ؽ�ƮUI
    public Text highScoreText; //�ְ� ���� �ؽ�ƮUI
    public TMP_Text resultScoreText; // ���â ��������
    public TMP_Text resultHighScoreText; // ���â �ְ�����
    public static bool hasShownGuide = false; //���̵� ���� 1ȸ�� true

    private float currentScore = 0f;
    private float highScore = 0f;
    
    private bool isGameOver = false;

    public float timeElapsed = 0f;
    public int difficultyLevel = 0;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //���� �ε�Ǹ� �ʱ�ȭ
        ResetGame();
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // �ν��Ͻ� ���� ����
        if (_instance == this)
            _instance = null;
    }

    private void Start()
    {
        // ���� ���� ���̵� ǥ��
        if (!hasShownGuide)
        {
            hasShownGuide = true;
            if (gameGuideUI != null)
            {
                gameGuideUI.SetActive(true); // ó�� ���� �ø� ǥ��
                Time.timeScale = 0f; // ���̵� �˾��϶� ���� ����
            }
            else
            { 
                Time.timeScale = 1f; // ���̵� �˾��� ���� ��� ���� ����
            }
        }

        InitializeUIReferences();
        currentHealth = maxHealth;
        UpdateHealthBar(); //���� �� ü�¹� �ʱ�ȭ
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        UpdateScoreUI();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!isGameOver)
        {
            currentScore += Time.deltaTime * 10f;
            UpdateScoreUI();

        }
        // 20�ʸ��� ���̵� ����
        if ((int)(timeElapsed / 20f) > difficultyLevel)
        {
            difficultyLevel++;
            Debug.Log("���̵� ����! ���� ����: " + difficultyLevel);
        }

    }
    public void InitializeUIReferences()
    {
        // UI ��ҵ��� ã�Ƽ� ������ �Ҵ�
        GameObject uiObject = GameObject.Find("UI");
        if (uiObject != null)
        {
            // �� UI ��Ҹ� ���� ã�Ƽ� �Ҵ�
            Transform healthBarTransform = uiObject.transform.Find("HealthBar");
            if (healthBarTransform != null)
                healthBar = healthBarTransform.GetComponent<Slider>();

            Transform scoreTextTransform = uiObject.transform.Find("ScoreText");
            if (scoreTextTransform != null)
                scoreText = scoreTextTransform.GetComponent<Text>();

            Transform highScoreTextTransform = uiObject.transform.Find("HighScoreText");
            if (highScoreTextTransform != null)
                highScoreText = highScoreTextTransform.GetComponent<Text>();
        }

        // ���â ��� ã��
        deathUI = Resources.FindObjectsOfTypeAll<GameObject>()
                 .FirstOrDefault(g => g.name == "DeathUI");

        if (deathUI != null)
        {
            Transform resultScoreTransform = deathUI.transform.Find("ResultScore");
            if (resultScoreTransform != null)
                resultScoreText = resultScoreTransform.GetComponent<TMP_Text>();

            Transform resultHighScoreTransform = deathUI.transform.Find("ResultHighScore");
            if (resultHighScoreTransform != null)
                resultHighScoreText = resultHighScoreTransform.GetComponent<TMP_Text>();

            deathUI.SetActive(false);
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
        scoreText.text = $"Score: {(int)currentScore}";
    if (highScoreText != null)
        highScoreText.text = $"Best: {(int)highScore}";
}

    public void GameOverScoreCheck() //��������� ����Ȯ��
    {
        isGameOver = true;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        ScoreManager.SaveScore((int)currentScore); //Ÿ��Ʋ�� ������ŷ�� ����
        PlayerPrefs.Save();

        UpdateScoreUI();

        if (resultScoreText != null) //���â�� �������� ǥ��
            resultScoreText.text = $"�������� : {(int)currentScore}";
        if (resultHighScoreText != null) // ���â�� �ְ����� ǥ��
            resultHighScoreText.text = $"�ְ����� : {(int)highScore}";

        if (scoreText != null) //���â ��½� ������ ����� ���������� �ְ����� UI ����
            scoreText.gameObject.SetActive(false);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(false);

        if (deathUI != null) // ���ӿ��� UI ǥ��
            deathUI.SetActive(true);
    }
        public void ResetGame()
    {
        InitializeUIReferences();
        ResetHealth(); // ü�� �ʱ�ȭ
        currentScore = 0f; // ���� �ʱ�ȭ
        timeElapsed = 0f; // �ð� �ʱ�ȭ
        difficultyLevel = 0; //���̵� �ʱ�ȭ


        isGameOver = false;
        if (scoreText != null)
            scoreText.gameObject.SetActive(true);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(true);
        //UI �ʱ�ȭ
        UpdateScoreUI();

        //���� ���� �г� ����
        if (deathUI != null)
            deathUI.SetActive(false);

        
        Time.timeScale = 1f; // ���� �ð� �帣��
    }

    public void AddScore(int amount) //���� ������ ���� �߰�
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    public void CloseGuide()
    {
        if (gameGuideUI != null)
            gameGuideUI.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ReturnToTitle() // Ÿ��Ʋ �ε��
    { 
        SceneManager.LoadScene("TitleScene");
    }
}
