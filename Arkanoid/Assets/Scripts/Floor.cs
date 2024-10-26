using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("Move")]
    public Vector3 offset;
    public int life;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tags.ball.ToString()))
        {
            life--;
            gameObject.transform.position += offset;
        }
        if(life <= 0&&!GameManager.LevelClear)
        {
            GameManager.ReloadThisScene();
        }
            
    }
}
