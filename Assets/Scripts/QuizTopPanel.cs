using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class QuizTopPanel
{
    private Text scoreText;
    private Text timerText;
    private List<Image> lifeImageList;
            
    public QuizTopPanel(Text scoreText, Text timerText, List<Image> lifeImageList)
    {
        this.scoreText = scoreText;
        this.timerText = timerText;
        this.lifeImageList = lifeImageList;
    }
    
    public void SetTime(string text)
    {
        timerText.text = text;   
    }
    
    public void SetScore(string text)
    {
        scoreText.text = text;
    }
    
    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }
}
