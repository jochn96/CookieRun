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
                //인스턴스가 없으면 씬에서 찾기
                _instance = FindObjectOfType<GameManager>();

                //씬에도 없으면 새로 생성
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

    public Text scoreText;  //현재 점수 텍스트UI
    public Text highScoreText; //최고 점수 텍스트UI
    public TMP_Text resultScoreText; // 결과창 현재점수
    public TMP_Text resultHighScoreText; // 결과창 최고점수
    public static bool hasShownGuide = false; //가이드 최초 1회만 true

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
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //씬이 로드되면 초기화
        ResetGame();
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // 인스턴스 참조 제거
        if (_instance == this)
            _instance = null;
    }

    private void Start()
    {
        // 게임 시작 가이드 표시
        if (!hasShownGuide)
        {
            hasShownGuide = true;
            if (gameGuideUI != null)
            {
                gameGuideUI.SetActive(true); // 처음 실행 시만 표시
                Time.timeScale = 0f; // 가이드 팝업일때 게임 정지
            }
            else
            { 
                Time.timeScale = 1f; // 가이드 팝업이 없을 경우 게임 진행
            }
        }

        InitializeUIReferences();
        currentHealth = maxHealth;
        UpdateHealthBar(); //시작 시 체력바 초기화
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
        // 20초마다 난이도 증가
        if ((int)(timeElapsed / 20f) > difficultyLevel)
        {
            difficultyLevel++;
            Debug.Log("난이도 증가! 현재 레벨: " + difficultyLevel);
        }

    }
    public void InitializeUIReferences()
    {
        // UI 요소들을 찾아서 변수에 할당
        GameObject uiObject = GameObject.Find("UI");
        if (uiObject != null)
        {
            // 각 UI 요소를 직접 찾아서 할당
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

        // 결과창 요소 찾기
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
        scoreText.text = $"Score: {(int)currentScore}";
    if (highScoreText != null)
        highScoreText.text = $"Best: {(int)highScore}";
}

    public void GameOverScoreCheck() //게임종료시 점수확인
    {
        isGameOver = true;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        ScoreManager.SaveScore((int)currentScore); //타이틀씬 점수랭킹에 저장
        PlayerPrefs.Save();

        UpdateScoreUI();

        if (resultScoreText != null) //결과창에 현재점수 표시
            resultScoreText.text = $"현재점수 : {(int)currentScore}";
        if (resultHighScoreText != null) // 결과창에 최고점수 표시
            resultHighScoreText.text = $"최고점수 : {(int)highScore}";

        if (scoreText != null) //결과창 출력시 오른쪽 상단의 현재점수와 최고점수 UI 숨김
            scoreText.gameObject.SetActive(false);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(false);

        if (deathUI != null) // 게임오버 UI 표시
            deathUI.SetActive(true);
    }
        public void ResetGame()
    {
        InitializeUIReferences();
        ResetHealth(); // 체력 초기화
        currentScore = 0f; // 점수 초기화
        timeElapsed = 0f; // 시간 초기화
        difficultyLevel = 0; //난이도 초기화


        isGameOver = false;
        if (scoreText != null)
            scoreText.gameObject.SetActive(true);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(true);
        //UI 초기화
        UpdateScoreUI();

        //게임 오버 패널 끄기
        if (deathUI != null)
            deathUI.SetActive(false);

        
        Time.timeScale = 1f; // 게임 시간 흐르게
    }

    public void AddScore(int amount) //코인 먹을시 점수 추가
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

    public void ReturnToTitle() // 타이틀 로드씬
    { 
        SceneManager.LoadScene("TitleScene");
    }
}
