using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }
    
    void Start()
    {
        scoreText.gameObject.SetActive(false);
        
        if (GameManager.FinalScore > 0)
        {
            
            scoreText.gameObject.SetActive(true);
            scoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
    
    private void StartGame()
    {
        {
            GameManager.Score = 0;
            GameManager.Lives = 4;
            GameManager.CurrentLevelNumber = 1;
            SceneManager.LoadScene("Scenes/game_level");
        }
    }
}