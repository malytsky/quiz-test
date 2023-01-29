using UnityEngine.UI;

public class QuizTopPanel
{
    private readonly Text scoreText, timerText;
    public QuizTopPanel(Text scoreText, Text timerText)
    {
        this.scoreText = scoreText;
        this.timerText = timerText;
    }
    
    public void SetTime(string text)
    {
        timerText.text = text;   
    }
    
    public void SetScore(string text)
    {
        scoreText.text = text;
    }
}
    
