using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public static float ElapsedTime;

    public GameObject[] lifeIcons;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject levelIntroPanel;
    public TextMeshProUGUI levelIntroText;


    private bool timerRunning = true;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (timerRunning)
        {
            ElapsedTime += Time.deltaTime;
            timerText.text = "Time " + Mathf.FloorToInt(ElapsedTime);
        }
    }

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            // Icon is "on" if its index is less than current lives, "off" otherwise
            lifeIcons[i].SetActive(i < lives -1);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score " + score;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }
    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        ElapsedTime = 0f;
    }
    public IEnumerator ShowLevelIntro(int levelNumber)
    {
        StopTimer();
        levelIntroPanel.SetActive(true);
        levelIntroText.text = "LEVEL " + levelNumber;

        yield return new WaitForSeconds(3f); // however long you want it shown
        StartTimer();
        levelIntroPanel.SetActive(false);
        levelIntroText.text = "";
    }
}