using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Medal {
    none,
    bronze,
    silver,
    gold,
    platinum
}
public class ScoreManager : MonoBehaviour
{
    public int score;
    public int bestScore;

    public Medal currentMedal;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        setCurrentMedal();
    }

    // Update is called once per frame
    public void AddScore()
    {
        score++;
        GameManager.Instance.uiManager.UpdateScore(score);
    }

    public void ResetScore()
    {
        score = 0;
        // score = 327;
        GameManager.Instance.uiManager.UpdateScore(score);
    }

    public void SaveScore()
    {
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            bestScore = score;
            setCurrentMedal();
        }
    }

    private void setCurrentMedal(){
        if(bestScore >= 50){
            currentMedal = Medal.bronze;
        } else if (bestScore >= 100) {
            currentMedal = Medal.silver;
        } else if (bestScore >= 200) {
            currentMedal = Medal.gold;
        } else if (bestScore >= 300) {
            currentMedal = Medal.platinum;
        } else {
            currentMedal = Medal.none;
        }
    }
}
