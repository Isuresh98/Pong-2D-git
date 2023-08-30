using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BallBounc : MonoBehaviour
{
    public Ball ball;
    public int gemsaddtest;

    private GameManager gameManager;
    private GemsManager gemsmanager;


    private void Start()
    {

        gameManager = GameManager.instance;
        gemsmanager = FindObjectOfType<GemsManager>();
        gemsmanager.AddGems(gemsaddtest);
    }
    private void Bounce(Collision2D collision)
    {
        Vector3 ballPosition = transform.position;
        Vector3 paddlePosition = collision.transform.position;
        float paddleHeight = collision.collider.bounds.size.x;

        float PositionY;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            PositionY = 1;
        }
        else
        {
            PositionY = -1;
        }

        float PositionX = (ballPosition.x - paddlePosition.x) / paddleHeight;

        ball.IncreesHitCount();
        ball.MoveBall(new Vector2(PositionX, PositionY));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Bounce(collision);
            Vibration.Vibrate(45);

        }
        if (collision.gameObject.CompareTag("Side"))
        {

            Vibration.Vibrate(45);

        }

        else if (collision.gameObject.CompareTag("DownBoundry"))
        {
            ball.playerStart = false;
            StartCoroutine(ball.Lauch());
            gameManager.DecrementHealth(1);
           // gemsmanager.ResetBallScale();
           // gemsmanager.ResetWidePaddle();
        }
    }
    private void Update()
    {

    }
}