using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizEndManager : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private Text scoreText, timerText, resultText, correctAnswers;
    
    private QuizTopPanel topPanel; 

    private void Start()
    {
        if (btnMenu != null)
        {
            btnMenu.onClick.AddListener(OnMenuButtonClick);
        }
        
        topPanel = new QuizTopPanel(scoreText, timerText); 
        topPanel.SetScore(QuizVariables.TextScore + QuizVariables.Score);
        var time = TimeSpan.FromSeconds(QuizVariables.Time);
        topPanel.SetTime(QuizVariables.TextTime + time.ToString("mm':'ss"));

        if (QuizVariables.CorrectAnswers < QuizVariables.QuestionNums)
        {
            correctAnswers.gameObject.SetActive(true);
            correctAnswers.text = QuizVariables.TextCorrectAnswers + QuizVariables.CorrectAnswers;
            resultText.text = QuizVariables.TextForLose;
        }
        else
        {
            correctAnswers.gameObject.SetActive(false);
            resultText.text = QuizVariables.TextForWin;
        }
    }

    private void Clear()
    {
        topPanel = null;
    }

    private void OnMenuButtonClick()
    {
        Clear();
        SceneManager.LoadScene("GameStart");
    }

}