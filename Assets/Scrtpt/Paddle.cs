using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCamera;

    private bool isBeingTouched = false;

    public float paddleLimit = 5f; // Set the desired limit for left and right movement

    private void Start()
    {
        gameManager = GameManager.instance;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isBeingTouched)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                float clampedX = Mathf.Clamp(touchPosition.x, -paddleLimit, paddleLimit);
                transform.position = new Vector2(clampedX, transform.position.y);
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.touchCount > 0)
        {
            isBeingTouched = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingTouched = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Increase score by 1
            gameManager.IncrementScore(1);
        }
    }
}