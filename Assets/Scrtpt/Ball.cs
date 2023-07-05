using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float Startspeed ;
    public float Extraspeed ;
    public float MaxExtraspeed ;

    public bool playerStart = true;

    private int HitCount=0;
    
    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Lauch());

    }

    public void ResetBall()
    {
        rigidbody.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
    }

    public IEnumerator Lauch()
    {
        ResetBall();
        HitCount = 0;
        yield return new WaitForSeconds(1);
        if (playerStart == true)
        {
            MoveBall(new Vector2(0, -1));


        }
        else
        {
            MoveBall(new Vector2(0, 1));

        }
    }   
    public void MoveBall(Vector2 Diraction)

    {
        Diraction = Diraction.normalized;
        float ballSpeed = Startspeed + HitCount * Extraspeed;
        rigidbody.velocity = Diraction * ballSpeed;


    }

    public void IncreesHitCount()
    {
        if (HitCount * Extraspeed < MaxExtraspeed)
        {
            HitCount++;
        }
    }
}
