using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public Image stopButtonImage;
    public Sprite pausedStateSprite;
    public Sprite resumedStateSprite;


    public float timerDuration = 10f; // Duration of the timer in seconds
    private float remainingTime; // Remaining time on the timer

    public Button PowerUpOnScreen;

    #region Portrait Mode Variables
    [Header("Potratemode")]
    //UI Potrate mode pannel set 
    public GameObject portraitPanel;
    public Text scoreText;
    public Text healthText;
    public GameObject gameEndPanel;
    public GameObject leaderboardPanel;
    public GameObject nameInputPanel;
    public GameObject scoreOverPanel;
    public GameObject StartBTPanel;
    public GameObject gemPanel;
    public Text gameOverScoreText;
    public Button continueButton;
    public Button leaderboardButton;
    public Button leaderboardCloseButton;
    public Button powerUpButton;
    public Button powerUpButtonClose;
    public Button ScoreGameoverButtonClose;
    public Button StartBT;
    public Button restBT;
    public Button StopBT;
    public Button gemBT;
    public Button gemBTClose;
    private const string NameInputFlagKey = "NameInputFlag";
    public GameObject PowerUppannel;
    private int playerScore;
    private int playerHealth = 1;
    public int scoreLimitInGemget;
    public int scoreLimitInGemgetCount;
    private bool isSubmittingScores = false;
    private List<int> scoresToUpload = new List<int>();
    private bool isGamePaused = false;
    [Header("lederBordPotrate")]
    private ScoreSaveBoard scoreSaveBoard;
    public Leaderboard ledarboard;
    private GemsManager gemsmanager;
    #endregion

    #region Landscape Mode Variables
    [Header("Lanscapemode")]

    public GameObject landscapePanel;
    public Text scoreText2;
    public Text healthText2;
    public GameObject gameEndPanel2;
    public GameObject leaderboardPanel2;
    public GameObject nameInputPanel2;
    public GameObject scoreOverPanel2;
    public GameObject StartBTPanel2;
    public GameObject gemPanel2;
    public Text gameOverScoreText2;
    public Button continueButton2;
    public Button leaderboardButton2;
    public Button leaderboardCloseButton2;
    public Button powerUpButton2;
    public Button powerUpButtonClose2;
    public Button ScoreGameoverButtonClose2;
    public Button StartBT2;
    public Button restBT2;
    public Button StopBT2;
    public Button gemBT2;
    public Button gemBTClose2;
    private const string NameInputFlagKey2 = "NameInputFlag";
    public GameObject PowerUppannel2;
    private int playerScore2;
    private int playerHealth2 = 1;
    public int scoreLimitInGemget2;
    public int scoreLimitInGemgetCount2;
    private bool isSubmittingScores2 = false;
    private List<int> scoresToUpload2 = new List<int>();
    private bool isGamePaused2 = false;
    [Header("lederBordPotrate")]
    private ScoreSaveBoard scoreSaveBoard2;
    public Leaderboard ledarboard2;
    private GemsManager gemsmanager2;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        scoreLimitInGemgetCount = 0;
        playerHealth = 1;
        UpdateScoreText();
        UpdateHealthText();
        gemsmanager = FindObjectOfType<GemsManager>();

        stopButtonImage = StopBT.GetComponent<Image>();
        if (PlayerPrefs.HasKey(NameInputFlagKey))
        {
            // Player has already entered their name, hide the name input panel
            nameInputPanel.SetActive(false);
            StartGame();
        }
        else
        {
            // Player hasn't entered their name yet, show the name input panel
            nameInputPanel.SetActive(true);
            StartBTPanel.SetActive(false);
            gameEndPanel.SetActive(false);
            leaderboardPanel.SetActive(false);
            PowerUppannel.SetActive(false);
            scoreOverPanel.SetActive(false);
            Time.timeScale = 0;
            StartBT.onClick.AddListener(StartGamePlayer);
            //lancape
            // Player hasn't entered their name yet, show the name input panel
            nameInputPanel2.SetActive(true);
            StartBTPanel2.SetActive(false);
            gameEndPanel2.SetActive(false);
            leaderboardPanel2.SetActive(false);
            PowerUppannel2.SetActive(false);
            scoreOverPanel2.SetActive(false);
            Time.timeScale = 0;
            StartBT2.onClick.AddListener(StartGamePlayer);
        }

        Time.timeScale = 0;

        continueButton.onClick.AddListener(ContinueGame);
        leaderboardButton.onClick.AddListener(OpenLeaderboard);
        leaderboardCloseButton.onClick.AddListener(CloseLeaderboard);
        powerUpButton.onClick.AddListener(powerUp);
        PowerUpOnScreen.onClick.AddListener(powerUp);
        powerUpButtonClose.onClick.AddListener(powerUpClose);
        ScoreGameoverButtonClose.onClick.AddListener(gameoverCloeBT);
        StartBT.onClick.AddListener(StartBTGamePlayer);
        restBT.onClick.AddListener(restarBT);
        StopBT.onClick.AddListener(ToggleGamePause);
        gemBT.onClick.AddListener(gemPannelOpen);
        gemBTClose.onClick.AddListener(gemPannelOpenClose);
        // Find and assign the ScoreSaveBoard component
        scoreSaveBoard = FindObjectOfType<ScoreSaveBoard>();
        StartCoroutine(StartTimer());
        //lanscape

        continueButton2.onClick.AddListener(ContinueGame);
        leaderboardButton2.onClick.AddListener(OpenLeaderboard);
        leaderboardCloseButton2.onClick.AddListener(CloseLeaderboard);
        powerUpButton2.onClick.AddListener(powerUp);
        powerUpButtonClose2.onClick.AddListener(powerUpClose);
        ScoreGameoverButtonClose2.onClick.AddListener(gameoverCloeBT);
        StartBT2.onClick.AddListener(StartBTGamePlayer);
        restBT2.onClick.AddListener(restarBT);
        StopBT2.onClick.AddListener(ToggleGamePause);
        gemBT2.onClick.AddListener(gemPannelOpen);
        gemBTClose2.onClick.AddListener(gemPannelOpenClose);
        // Find and assign the ScoreSaveBoard component
        scoreSaveBoard2 = FindObjectOfType<ScoreSaveBoard>();
        StartCoroutine(StartTimer());
    }
    private void Update()
    {
        if (scoreLimitInGemgetCount >= scoreLimitInGemget)
        {
            scoreLimitInGemgetCount = 0;
            // Perform the desired action when scoreLimitInGemgetCount reaches the limit
            // For example, call a method or trigger an event
            PerformAction();
        }

      
    }
    private void PerformAction()
    {
        // Add your code here to perform the desired action when scoreLimitInGemgetCount reaches the limit
        gemsmanager.AddGems(1);
    }
    public IEnumerator StartTimer()
    {
        remainingTime = timerDuration;

        while (remainingTime > 0f)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            remainingTime--;

        }
        powerUpButton.gameObject.SetActive(true); // Activate the power-up button after the timer elapses 
        powerUpButton2.gameObject.SetActive(true); // Activate the power-up button after the timer elapses 
        // Timer has finished
        TimerFinished();
    }

    private void TimerFinished()
    {
        // Perform any actions you want when the timer finishes
        Debug.Log("Timer finished!");
    }

    public void IncrementScore(int value)
    {
        playerScore += value;
        scoreLimitInGemgetCount += value;
        UpdateScoreText();

        // Save the score
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.Save();

        /* // Update the score in the ScoreSaveBoard script
         scoreSaveBoard.UpdateScore(playerScore);*/
    }


    public void DecrementHealth(int value)
    {
        playerHealth -= value;
        UpdateHealthText();

        if (playerHealth <= 0)
        {
            EndGame();
        
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore;
        scoreText2.text = "Score: " + playerScore;
        //add 



    }

    private void UpdateHealthText()
    {
        healthText.text = playerHealth.ToString();
        healthText2.text = playerHealth.ToString();

    }

    private void ContinueGame()
    {
        // Reset the player's health and score
        playerHealth = 1;
        UpdateHealthText();
        UpdateScoreText();
        Time.timeScale = 1;

        // Hide the game over panel
        gameEndPanel.SetActive(false);
        gameEndPanel2.SetActive(false);

        // Implement logic to resume the game or start a new round
        // For example, show an ad when the continueButton is clicked
        /* if (adManager != null)
         {
             adManager.ShowInterstitialAd();
         }*/

        AdsManager.Instance.ShowAd();
    }

    private void EndGame()
    {
        gameEndPanel.SetActive(true);
        gameEndPanel2.SetActive(true);
        Time.timeScale = 0;
        scoreOverPanel.SetActive(true);
        scoreOverPanel2.SetActive(true);

        // Update game over score text
        gameOverScoreText.text = playerScore.ToString();
        gameOverScoreText2.text = playerScore.ToString();

        // Add the score to the list of scores to upload
        scoresToUpload.Add(playerScore);
        scoresToUpload2.Add(playerScore);

        // Upload the scores one by one

        // Send the score value to the ScoreSaveBoard script
       /* scoreSaveBoard.AddScoreEntry(scoresToUpload.Count, playerScore);
        scoreSaveBoard2.AddScoreEntry(scoresToUpload2.Count, playerScore);*/
        StartCoroutine(ledarboard.SubmitScoreRoutine(playerScore));
        StartCoroutine(ledarboard2.SubmitScoreRoutine(playerScore));
    }




    private void powerUp()
    {
        PowerUppannel.SetActive(true);
        gameEndPanel.SetActive(false);
        PowerUppannel2.SetActive(true);
        gameEndPanel2.SetActive(false);
        Time.timeScale = 0;
        // powerUpButton.gameObject.SetActive(false); // Activate the power-up button after the timer elapses 

    }
    private void powerUpClose()
    {
        PowerUppannel.SetActive(false);
        gameEndPanel.SetActive(true);
        PowerUppannel2.SetActive(false);
        gameEndPanel2.SetActive(true);
        Time.timeScale = 1;
        // Hide the game over panel
        gameEndPanel.SetActive(false);
        gameEndPanel2.SetActive(false);
    }

    private void gemPannelOpen()
    {
        gemPanel.SetActive(true);
        gameEndPanel.SetActive(false);
        gemPanel2.SetActive(true);
        gameEndPanel2.SetActive(false);


    }
    private void gemPannelOpenClose()
    {
        gemPanel.SetActive(false);
        gameEndPanel.SetActive(true);
        gemPanel2.SetActive(false);
        gameEndPanel2.SetActive(true);
    }


    
    private void OpenLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        leaderboardPanel2.SetActive(true);

    }

    private void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
        leaderboardPanel2.SetActive(false);


    }
    public void StartGamePlayer()
    {

        // Hide the name input panel
        nameInputPanel.SetActive(false);
        nameInputPanel2.SetActive(false);

        // Set the flag indicating that the name input is completed
        PlayerPrefs.SetInt(NameInputFlagKey, 1);
        PlayerPrefs.Save();

        // Start the game
        StartGame();
    }
    private void StartGame()
    {
        Time.timeScale = 0;

        gameEndPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        PowerUppannel.SetActive(false);
        scoreOverPanel.SetActive(false);
        playerHealth = 1;
        playerScore = 0;
        UpdateHealthText();
        UpdateScoreText();
        StartBTPanel.SetActive(true);
        scoreLimitInGemgetCount = 0;


        gameEndPanel2.SetActive(true);
        leaderboardPanel2.SetActive(false);
        PowerUppannel2.SetActive(false);
        scoreOverPanel2.SetActive(false);
        playerHealth2 = 1;
        playerScore2 = 0;
        StartBTPanel2.SetActive(true);
        scoreLimitInGemgetCount2 = 0;
    }
    public void StartBTGamePlayer()
    {
        nameInputPanel.SetActive(false);

        gameEndPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        PowerUppannel.SetActive(false);
        StartBTPanel.SetActive(false);
        playerHealth = 1;
        playerScore = 0;
        UpdateHealthText();
        UpdateScoreText();

        Time.timeScale = 1;


        nameInputPanel2.SetActive(false);

        gameEndPanel2.SetActive(false);
        leaderboardPanel2.SetActive(false);
        PowerUppannel2.SetActive(false);
        StartBTPanel2.SetActive(false);
        playerHealth2 = 1;
        playerScore2 = 0;
    }
    public void restarBT()
    {
        /* scoreOverPanel.SetActive(false);
         StartGamePlayer();
         playerHealth = 3;
         playerScore = 0;
         UpdateHealthText();
         UpdateScoreText();
         Time.timeScale = 1;*/
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    public void gameoverCloeBT()
    {
        scoreOverPanel.SetActive(false);
        StartBTPanel.SetActive(true);
        scoreOverPanel2.SetActive(false);
        StartBTPanel2.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //Game stop Contraller
    private void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            PauseGame();
            // Change the image to the paused state image
            stopButtonImage.sprite = pausedStateSprite;
        }
        else
        {
            ResumeGame();
            // Change the image to the resumed state image
            stopButtonImage.sprite = resumedStateSprite;
        }
    }


    private void PauseGame()
    {
        Time.timeScale = 0f; // Stop the game's time
        DisablePlayerInput(); // Disable player input
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game's time
        EnablePlayerInput(); // Enable player input
    }

    private void DisablePlayerInput()
    {
        // Disable player control logic here
        // For example, you can disable player movement scripts, input handlers, etc.
    }

    private void EnablePlayerInput()
    {
        // Enable player control logic here
        // For example, you can enable player movement scripts, input handlers, etc.
    }

    public void LanscapeOn()
    {
        landscapePanel.SetActive(true);
        portraitPanel.SetActive(false);
    }
    public void potrateOn()
    {
        landscapePanel.SetActive(false);
        portraitPanel.SetActive(true);
    }
}