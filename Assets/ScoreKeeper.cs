using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private int score; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    private float timeLeft;
    private bool timeStarted;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timeLeft = 120;
        timeStarted = false;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+ score;
        if(timeStarted && !finished){
        timeLeft -= Time.deltaTime;
        }
        if(timeLeft<0){
            finished = true;
            timeLeft = 0f;
        }
        timeText.text = "Time left: " + timeLeft;
    }
    public void IncreaseScore(int inc) {
        if (!finished)
        score += inc;
    }
    public void startTime(){
        timeStarted = true;
    }
    public int GetScore()
    {
        return score;
    }
}
