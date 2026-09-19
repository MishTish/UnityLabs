using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int FinalScore;
    public static int CurrentLevelNumber  = 1;
    public static bool WinStatus;
    public static int Score;
    public static int Lives = 4;

    public int brickCount;
    public float gameDelay = 3f;
    public bool canMove = true;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UIManager.Instance.UpdateLives(Lives);
        UIManager.Instance.UpdateScore(Score);
    }
    
    IEnumerator GameOver() 
    {
        canMove = false;
        UIManager.Instance.StopTimer();
        yield return new WaitForSeconds(gameDelay);
        SaveScore();
        SceneManager.LoadScene("Scenes/game_over");
    }

    IEnumerator LevelClear()
    {
        canMove = false;
        BallLogic.Instance.speed = 0f;
        UIManager.Instance.StopTimer();
        CurrentLevelNumber++;
        yield return new WaitForSeconds(gameDelay);
        if (CurrentLevelNumber == 6)
        {
            WinStatus = true;
            SaveScore();

            SceneManager.LoadScene("Scenes/game_over");
        }
        else
        {
            SceneManager.LoadScene("Scenes/game_level");
        }
    }
    public void LoseLife()
    {
        Lives--;
        UIManager.Instance.UpdateLives(Lives);
        Debug.Log("Lives remaining: " + Lives);

        if (Lives <= 0)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            BallLogic.Instance.ResetBall();
        }
    }

    public void BrickDestroyed()
    {
        brickCount--;
        if (brickCount == 0)
        {
            StartCoroutine(LevelClear());
        }
    }
    public void AddScore(int amount)
    {
        Score += amount;
        UIManager.Instance.UpdateScore(Score);
        Debug.Log("Score: " + Score);
    }
    void SaveScore()
    {
        FinalScore = Score;
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (FinalScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", FinalScore);
        }
    }
}