﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Text scoreText;
    int score;

    Rigidbody2D ballRigidbody2D;
    CircleCollider2D ballCircleCollider2D;

    [Header("水平速度")]
    public float speedX;

    [Header("垂直速度")]
    public float speedY;

    

    enum tags
    {
        wall,
        brick
    }

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballCircleCollider2D = GetComponent<CircleCollider2D>();

        

        scoreText.text = "score：";
        Invoke("ballStart", 1);
    }

    void Update()
    {
        
        velocityX = ballRigidbody2D.velocity.x;
        velocityY = ballRigidbody2D.velocity.y;
       
        if (Input.GetKey(KeyCode.Space))
        {
            ballStart();
        }
    }

    void ballStart()
    {
        if (isStop())
        {
            ballCircleCollider2D.enabled = true;
            transform.SetParent(null);
            ballRigidbody2D.velocity = new Vector2(speedX, speedY);

            
            ballRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    bool isStop()
    {
        return ballRigidbody2D.velocity == Vector2.zero;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        lockSpeed();
        if (other.gameObject.CompareTag(tags.brick.ToString()))
        {
            GameManager.brickscount--;
            Debug.Log("brickscountnow"+GameManager.brickscount);
            GameManager.CheckLevelClearOrNot();
            other.gameObject.SetActive(false);
            score += 10;
            scoreText.text = "score：" + score;
        }

    }

    void lockSpeed()
    {
        Vector2 lockSpeed = new Vector2(resetSpeedX(), resetSpeedY());
        ballRigidbody2D.velocity = lockSpeed;
    }

    float resetSpeedX()
    {
        float currentSpeedX = ballRigidbody2D.velocity.x;
        if (currentSpeedX < 0)
        {
            return -speedX;
        }
        else
        {
            return speedX;
        }
    }

    float resetSpeedY()
    {
        float currentSpeedY = ballRigidbody2D.velocity.y;
        if (currentSpeedY < 0)
        {
            return -speedY;
        }
        else
        {
            return speedY;
        }
    }
}

