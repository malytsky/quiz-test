using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGameTopPanel : QuizTopPanel
{
    private readonly List<Image> lifeImageList;
            
    public QuizGameTopPanel(Text scoreText, Text timerText, List<Image> lifeImageList) : base(scoreText, timerText)
    {
        this.lifeImageList = lifeImageList;
    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }
}
