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

    //scriptable object file
    [SerializeField] private QuizDataScriptable quizData;

    private QuizOptions quizOptions;
    private QuizGameTopPanel quizTopPanel; 
    private QuizModel quizModel; 
    private float currentTime;

    private void Start()
    {
        //Using patterns Fabric and Builder
        quizOptions = new QuizOptions(options, correctCol, wrongCol, normalCol);  //create options 
        quizTopPanel = new QuizGameTopPanel(scoreText, timerText, lifeImageList);  //create top panel
        quizModel = new QuizModel();                                               //create model class for logic methods and operations

        quizOptions.OnAnswer += Answer;
        quizModel.OnSetScore += SetScore;
        quizModel.OnSetQuestion += SetQuestion;
        quizModel.OnReduceLife += ReduceLife;
        quizModel.OnGameEnd += GameEnd;
        quizModel.OnClear += Clear;

        StartGame();
    }
    
    private void StartGame()
    {
        currentTime = 0;
        quizModel.StartGame(quizData);
    }

    private void Clear()
    {
        quizOptions.OnAnswer -= Answer;
        quizModel.OnSetScore -= SetScore;
        quizModel.OnSetQuestion -= SetQuestion;
        quizModel.OnReduceLife -= ReduceLife;
        quizModel.OnGameEnd -= GameEnd;
        quizModel.OnClear -= Clear;
        
        quizOptions.Clear();
        quizOptions = null;
        quizTopPanel = null;
        quizModel = null;
    }

    private void GameEnd()
    {
        QuizVariables.Time = currentTime;
    }

    // display the question on the screen
    private void SetQuestion(Question question)
    {
        QuizQuestion.Init(question, questionInfoText, questionImg); //create question
        quizOptions.UpdateButtons(question.questionOptions);
    }

    private void SetScore(string text)
    {
        quizTopPanel.SetScore(text);
    }

    private void ReduceLife(int remainingLife) // if answer is wrong
    {
        quizTopPanel.ReduceLife(remainingLife);
    }

    private bool Answer(string selectedOption)
    {
        return quizModel.Answer(selectedOption);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        var time = TimeSpan.FromSeconds(currentTime);            //set the time and convert to Time format
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