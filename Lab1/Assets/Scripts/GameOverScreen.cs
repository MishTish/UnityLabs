using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI scoreText;
   public TextMeshProUGUI timeText;
    public float titleDelay = 3f;

    void Start()
    {
        scoreText.text = "SCORE: " + GameManager.FinalScore;
        timeText.text = "";

        if (GameManager.WinStatus)
        {
            gameOverText.text = "";
            winText.text = "WINNER";
            timeText.text = "TIME: " + Mathf.FloorToInt(UIManager.ElapsedTime);
        }
        else
        {
            gameOverText.text = "GAME OVER";
            winText.text = "";
        }
        UIManager.Instance.ResetTimer();
        StartCoroutine(TitleTransition());
    }

    IEnumerator TitleTransition()
    {
        yield return new WaitForSeconds(titleDelay);
        SceneManager.LoadScene("Scenes/title");
    }
    
}
