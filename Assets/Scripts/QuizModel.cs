using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class QuizModel
{
    private List<Question> questions; //questions data
    private Question currentQuestion; //current question data
    private int lives;
    private int score;
    private int correctAnswers;
    public event Action<string> OnSetScore;
    public event Action<Question> OnSetQuestion;
    public event Action<int> OnReduceLife;
    public event Action OnInvoking;
    public event Action OnGameEnd;
    public event Action OnClear;
    
    public void StartGame(QuizDataScriptable quizData)
    {
        score = 0;
        correctAnswers = 0;
        lives = QuizVariables.Lives;
        OnSetScore(QuizVariables.TextScore + score);
        questions = new List<Question>();

        SetQuestions(quizData);
        QuizVariables.QuestionNums = questions.Count;

        SelectQuestion();
    }

    private void SetQuestions(QuizDataScriptable quizData)
    {
        questions.AddRange(quizData.questions);
    }
    
    public void SelectQuestion()
    {
        //get the random or not random question from list
        int value = QuizVariables.RandomOrder ? Random.Range(0, questions.Count) : 0;
        //set the selected question
        currentQuestion = questions[value];
        //send the question to quizView
        OnSetQuestion?.Invoke(currentQuestion);

        questions.RemoveAt(value);
    }
    
    //called to check is the answer correct
    public bool Answer(string selectedOption)
    {
        bool correct = false;
  
        if (currentQuestion.questionAnswer == selectedOption) // check for answer
        {
            correct = true;
            score += QuizVariables.ScoreReward;
            OnSetScore(QuizVariables.TextScore + score);
            correctAnswers++;
        }
        else
        {
            //minus life
            lives--;
            OnReduceLife?.Invoke(lives);

            if (lives == 0)
            {
                GameEnd();
            }
        }
        
        if (questions.Count > 0)
        {
            OnInvoking?.Invoke();
        }
        else
        {
            GameEnd();
        }
            
        return correct;
    }

    private void Clear()
    {
        currentQuestion = null;
        questions.Clear();
        questions = null;
        OnClear?.Invoke();
    }

    private async void GameEnd()
    {
        await Task.Delay(200);
        QuizVariables.Score = score;
        QuizVariables.CorrectAnswers = correctAnswers;
        OnGameEnd?.Invoke();

        //Save score if it lesser than record 
        var record = PlayerPrefs.GetInt(QuizVariables.TextToSaveRecord);
        if (score > record)
        {
            PlayerPrefs.SetInt(QuizVariables.TextToSaveRecord, score);
        }
        Clear();
        SceneManager.LoadScene("GameOver");
    }
   
}
