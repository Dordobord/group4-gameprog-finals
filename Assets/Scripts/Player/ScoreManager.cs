using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI PlayerHealth;
    public int score, highscore;
    void Start()
    {
        score = 0;
        ResetScore();
        highScore.text = "Highest Score: " + PlayerPrefs.GetInt("HighScore");
        highscore = PlayerPrefs.GetInt("HighScore");
        PlayerHealth.text = "Health: 100";
        scoretext.text = "Score: " + PlayerPrefs.GetInt("HighScore");


    }
    public void FinalScore()
    {
        score = PlayerPrefs.GetInt("PlayerScore");
        highscore = PlayerPrefs.GetInt("HighScore");
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.Save();
    }
    public void ChangeHealthTxt(float PlayerHp)
    {
        PlayerHealth.text = PlayerHp.ToString();
    }
    private void Update()
    {
        highScore.text = "Highest Score: " + PlayerPrefs.GetInt("HighScore");
        PlayerHealth.text = "Health: " + PlayerPrefs.GetFloat("PlayerHP");
        scoretext.text = "Score: " + PlayerPrefs.GetInt("PlayerScore");
    }
}
