using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    private GameManager gameManager;
    public int gemsCount;
    public int paddleCount;
    public GameObject pongBall;
    public GameObject paddle;
    public Text gemsCountText;
    public Text gemsCountText2;
    public Text paddleCountText;

    private const int BallGemsPrice = 35;
    private const int PaddleGemsPrice = 100;
    private const float BallScaleFactor = 1.5f;
    private const float WidePaddleFactor = 1.5f;
    private bool isBallScaled = false;
    private bool isWidePaddle = false;
    private Vector3 originalPaddleScale;


    private void Start()
    {
        gameManager = GameManager.instance;
        LoadGemsCount();
      
        UpdateGemsCountText();
        originalPaddleScale = paddle.transform.localScale;
    }

    private void OnApplicationQuit()
    {
        SaveGemsCount();
       
    }

    public void BuyGemsToBall()
    {
        
        if (!isBallScaled && gemsCount >= BallGemsPrice)
        {
            gemsCount -= BallGemsPrice;
            ScaleUpBall();
            isBallScaled = true;
            Debug.Log("Ball scaled up successfully!");
            gameManager.StartCoroutine(gameManager.StartTimer());
            gameManager.powerUpButton.gameObject.SetActive(false);
        }
       
        else
        {
            Debug.Log("Not enough gems to make a purchase or the item is already activated.");
        }
        UpdateGemsCountText();
    }

    public void BuyGemsToPaddle()
    {
        if (!isWidePaddle && gemsCount >= PaddleGemsPrice)
        {
            gemsCount -= PaddleGemsPrice;
            ScaleUpPaddle();
            isWidePaddle = true;
            Debug.Log("Paddle width increased successfully!");
            gameManager.StartCoroutine(gameManager.StartTimer());
            gameManager.powerUpButton.gameObject.SetActive(false);


        }
        else
        {
            Debug.Log("Not enough gems to make a purchase or the item is already activated.");
        }
        UpdateGemsCountText();
    }

    private void ScaleUpBall()
    {
        if (pongBall != null)
        {
            pongBall.transform.localScale *= BallScaleFactor;
        }
    }

    private void ScaleUpPaddle()
    {
        if (paddle != null)
        {
            paddle.transform.localScale = new Vector3(originalPaddleScale.x * WidePaddleFactor, originalPaddleScale.y, originalPaddleScale.z);
            paddleCount = 1;
        }
    }

    public void ResetBallScale()
    {
        if (isBallScaled && pongBall != null)
        {
            pongBall.transform.localScale /= BallScaleFactor;
            isBallScaled = false;
        }
    }

    public void ResetWidePaddle()
    {
        if (isWidePaddle && paddle != null && paddleCount == 1)
        {
            paddle.transform.localScale = originalPaddleScale;
            isWidePaddle = false;
            paddleCount = 0;
            UpdateGemsCountText();
        }
    }

    private void UpdateGemsCountText()
    {
        if (gemsCountText != null|| gemsCountText2 != null)
        {
            gemsCountText.text = "" + gemsCount;
            gemsCountText2.text = "" + gemsCount;
        }
        if (paddleCountText != null)
        {
            paddleCountText.text = "" + paddleCount;
        }
    }

    private void SaveGemsCount()
    {
        PlayerPrefs.SetInt("GemsCount", gemsCount);
        PlayerPrefs.Save();
    }

    private void LoadGemsCount()
    {
        if (PlayerPrefs.HasKey("GemsCount"))
        {
            gemsCount = PlayerPrefs.GetInt("GemsCount");
        }
    }
    public void AddGems(int amount)
    {
        gemsCount += amount;
        UpdateGemsCountText();
        SaveGemsCount(); // Save the updated gems count
    }


}
