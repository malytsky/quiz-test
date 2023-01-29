using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QuizOptions
{
    private readonly Color correctCol, wrongCol, normalCol;
    private bool isAnswered;
    private readonly List<Button> options;
    public event Predicate<string> OnAnswer;
    public QuizOptions(List<Button> options, Color correctCol, Color wrongCol, Color normalCol)
    {
        this.correctCol = correctCol;
        this.wrongCol = wrongCol;
        this.normalCol = normalCol;
        this.options = options;

        //listener for all options
        foreach (var localBtn in options)
        {
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }
    
    //Options click
    private void OnClick(Selectable btn)
    {
        //if not answered before
        if (isAnswered) return;
        //set answered true
        isAnswered = true;
        //check if answer is correct
        var isTrueAnswer = OnAnswer != null && OnAnswer(btn.name);

        //blinking for true and wrong
        BlinkOption(btn.image, isTrueAnswer ? correctCol : wrongCol);
    }
    
    public void Clear()
    {
        foreach (var localBtn in options)
        {
            localBtn.onClick.RemoveAllListeners();
        }
    }

    public void UpdateButtons(List<string> questionOptions) 
    {
        //assign invisible unused option buttons
        for (int i = questionOptions.Count; i < options.Count; i++)
        {
            options[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < questionOptions.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<Text>().text = questionOptions[i];
            options[i].name = questionOptions[i];    //set the name of button
            options[i].image.color = normalCol; //set color of button to normal
            options[i].gameObject.SetActive(true);
        }
        isAnswered = false;
    }
    
    //this give blink effect [if needed use or dont use]
    private static async void BlinkOption(Graphic image, Color color)
    {
        Color[] colors = {Color.black, color, Color.black, color};
        foreach (var col in colors)
        {
            ChangeColor(image, col);
            await Task.Delay(100);
        }
    }

    private static void ChangeColor(Graphic image, Color color)
    {
        if (image != null)
        {
            image.color = color;
        }
    }
}
