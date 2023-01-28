using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizView : MonoBehaviour
{
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private Color correctCol, wrongCol, normalCol; //buttons color
    [SerializeField] private Image questionImg;                     //image component for question
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private Text questionInfoText;                 //text of question
    [SerializeField] private List<Button> options;                  //options button reference

    //scriptableobject file
    [SerializeField] private QuizDataScriptable quizData;

    private QuizOptions quizOptions; 
    private QuizQuestion quizQuestion; 
    private QuizTopPanel quizTopPanel; 
    private QuizModel quizModel; 
    private float currentTime;

    private void Start()
    {
        //Using patterns Fabric and Builder
        quizOptions = new QuizOptions(options, correctCol, wrongCol, normalCol, this); //create options 
        quizQuestion = new QuizQuestion();                                                    //create question
        quizTopPanel = new QuizTopPanel(scoreText, timerText, lifeImageList);                 //create top panel
        quizModel = new QuizModel(this);                                               //create model class for logic methods and operations
        StartGame();
    }
    
    private void StartGame()
    {
        currentTime = 0;
        quizModel.StartGame();
    }

    public void Clear()
    {
        quizOptions.Clear();
        quizOptions = null;
        quizQuestion = null;
        quizTopPanel = null;
        quizModel = null;
    }

    public void GameEnd()
    {
        QuizVariables.Time = currentTime;
    }

    // display the question on the screen
    public void SetQuestion(Question question)
    {
        quizQuestion.Init(question, questionInfoText, questionImg);
        quizOptions.UpdateButtons(question.questionOptions);
    }
    
    public void SetQuestions(List<Question> questions)
    {
        questions.AddRange(quizData.questions);
    }

    public void SetScore(string text)
    {
        quizTopPanel.SetScore(text);
    }
    
    public void ReduceLife(int remainingLife) // if answer is wrong
    {
        quizTopPanel.ReduceLife(remainingLife);
    }

    public bool Answer(string selectedOption)
    {
        return quizModel.Answer(selectedOption);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        SetTime();
    }

    public void Invoking()
    {
        Invoke("SelectQuestion", 0.4f);
    }

    private void SelectQuestion()
    {
        quizModel.SelectQuestion();
    }
    
    void SetTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);            //set the time and convert to Time format
        quizTopPanel.SetTime(QuizVariables.TextTime + time.ToString("mm':'ss"));
    }
}

//question structure for YAML-file
[Serializable]
public class Question
{
    public QuestionType questionType;   //type
    public string questionInfo;         //question text 
    public Sprite questionImage;        //image
    public List<string> questionOptions;//list of options
    public string questionAnswer;       //correct option
}

[Serializable]
public enum QuestionType
{
    Text,
    Image
}