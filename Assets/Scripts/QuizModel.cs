using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizModel
{
    private QuizView quizView;
    private List<Question> questions; //questions data
    private Question currentQuestion; //current question data
    private int lives;
    private int score;
    private int correctAnswers;
    
    public QuizModel(QuizView quizView)
    {
        this.quizView = quizView;
    }
    public void StartGame()
    {
        score = 0;
        correctAnswers = 0;
        lives = QuizVariables.Lives;
        quizView.SetScore(QuizVariables.TextScore + score);
        
        //set the questions data
        questions = new List<Question>();
        quizView.SetQuestions(questions);
        QuizVariables.QuestionNums = questions.Count;

        SelectQuestion();
    }
    public void SelectQuestion()
    {
        //get the random or not random question from list
        int value = QuizVariables.RandomOrder ? Random.Range(0, questions.Count) : 0;
        //set the selected question
        currentQuestion = questions[value];
        //send the question to quizView
        quizView.SetQuestion(currentQuestion);

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
            quizView.SetScore(QuizVariables.TextScore + score);
            correctAnswers++;
        }
        else
        {
            //minus life
            lives--;
            quizView.ReduceLife(lives);

            if (lives == 0)
            {
                GameEnd();
            }
        }
        
        if (questions.Count > 0)
        {
            quizView.Invoking();
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
        quizView.Clear();
        quizView = null;
    }
    
    async void GameEnd()
    {
        await Task.Delay(200);
        QuizVariables.Score = score;
        QuizVariables.CorrectAnswers = correctAnswers;
        quizView.GameEnd();
        
        //Save score if it lesser than record 
        int record = PlayerPrefs.GetInt(QuizVariables.TextToSaveRecord);
        if (score > record)
        {
            PlayerPrefs.SetInt(QuizVariables.TextToSaveRecord, score);
        }
        Clear();
        SceneManager.LoadScene("GameOver");
    }
   
}
