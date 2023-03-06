using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private int score; 
    public TextMeshProUGUI scoreText;
    private float timeLeft;
    private bool timeStarted;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timeLeft = 120;
        timeStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+ score;
        if(timeStarted)
        timeLeft -= Time.deltaTime;
    }
    public void IncreaseScore(int inc) {
        score += inc;
    }
    public void startTime(){
        timeStarted = true;
    }
}
