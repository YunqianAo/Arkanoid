using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("BricksCount")]
    public static int brickscount;
    public static GameObject nextLevelButton;
    public static GameObject victoryMessage;

    public static void ReloadThisScene() { 
        Scene current=SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
    public static bool LevelClear
    {
        get
        {
            if (brickscount == 0)
            {
                return true;
            }
            return false;
        }
    }
    public static void CheckLevelClearOrNot()
    {
        if (LevelClear)
        {
            if (IsFinalLevel())
            {
                Debug.Log("Game completed! Showing victory message.");
                ShowVictoryMessage();
                showNextLevelButton();
            }
            else
            {
                Debug.Log("nextlevel");
                showNextLevelButton();
            }
        }
    }
    public void GoToScene(string next)
    {
        SceneManager.LoadScene (next);
    }
    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton=GameObject.FindGameObjectWithTag(tags.nextnivel.ToString());
        nextLevelButton.SetActive(false);
        victoryMessage = GameObject.FindGameObjectWithTag(tags.win.ToString());
        if (victoryMessage != null)
        {
            victoryMessage.SetActive(false);
        }
        brickscount =GameObject.FindGameObjectsWithTag(tags.brick.ToString()).Length;
        Debug.Log("brickscount"+brickscount);
    }
    static void showNextLevelButton()
    {
        nextLevelButton.SetActive(true);
    }

    private static void ShowVictoryMessage()
    {
        if (victoryMessage != null)
        {
            victoryMessage.SetActive(true);
        }
    }

    private static bool IsFinalLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        return current.name == "Level2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
enum tags
{
    wall,
    brick,
    racket,
    nextnivel,
    ball,
    win
}
