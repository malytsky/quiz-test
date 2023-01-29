using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizEndManager : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private Text scoreText, timerText, resultText, correctAnswers;

    private void Start()
    {
        if (btnMenu != null)
        {
            btnMenu.onClick.AddListener(OnMenuButtonClick);
        }

        scoreText.text = QuizVariables.TextScore + QuizVariables.Score;
        
        var time = TimeSpan.FromSeconds(QuizVariables.Time);
        timerText.text = QuizVariables.TextTime + time.ToString("mm':'ss");

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

    private static void OnMenuButtonClick()
    {
        SceneManager.LoadScene("GameStart");
    }

}