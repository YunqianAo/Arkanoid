using System.Collections;
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

    #region 教學理解用 可不寫
    [Header("實際水平速度")]
    public float velocityX;

    [Header("實際垂直速度")]
    public float velocityY;
    #endregion

    enum tags
    {
        磚塊,
        背景
    }

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballCircleCollider2D = GetComponent<CircleCollider2D>();

        //切換成Kinematic模式
        //Uity 2018版以後不加這行的話 發球之前球會無法跟著球拍移動
        ballRigidbody2D.bodyType = RigidbodyType2D.Kinematic;

        scoreText.text = "目前分數：";
        Invoke("ballStart", 1);
    }

    void Update()
    {
        #region 教學理解用 可不寫
        velocityX = ballRigidbody2D.velocity.x;
        velocityY = ballRigidbody2D.velocity.y;
        #endregion

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

            //Unity 2018以後的版本需要加下面這行：
            //改回預設的Dynamic，使用Unity內建的物理運動規則
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
        if (other.gameObject.CompareTag(tags.磚塊.ToString()))
        {
            other.gameObject.SetActive(false);
            score += 10;
            scoreText.text = "目前分數：" + score;
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

