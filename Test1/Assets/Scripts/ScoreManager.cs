using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI highScoreText;
    public int scoreCount;
    public int highScoreCount;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore",0);
        }
        else
        {
            highScoreCount = PlayerPrefs.GetInt("HighScore");
        }
    }
    public void AddScore(int scoreToAdd)
    {
        scoreCount += scoreToAdd;
    }
    // Update is called once per frame
    void Update()
    {
        if(scoreCount>highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetInt("HighScore",highScoreCount);
        }
        scoretext.text = "Score: " + scoreCount;
        highScoreText.text = "High Score: " + highScoreCount;


    }
}
