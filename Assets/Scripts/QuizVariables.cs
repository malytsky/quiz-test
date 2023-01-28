using System;

public static class QuizVariables
{
    public const int Lives = 3;
    public const int ScoreReward = 50;
    public const string TextScore = "Счет:";
    public const string TextTime = "Время:";
    public const string TextRecord = "Рекорд:";
    public const string TextCorrectAnswers = "Верных ответов: ";
    public const string TextForLose = "Вы можете лучше";
    public const string TextForWin = "Вы победили";
    public const string TextToSaveRecord = "record";
        
    public static int Score;
    public static int CorrectAnswers;
    public static int QuestionNums;
    public static float Time;
    public static bool RandomOrder;
}