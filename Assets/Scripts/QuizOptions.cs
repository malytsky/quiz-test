using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QuizOptions
{
    private Color correctCol, wrongCol, normalCol;
    private bool isAnswered;
    private List<Button> options;
    private QuizView quizView;
    public QuizOptions(List<Button> options, Color correctCol, Color wrongCol, Color normalCol, QuizView quizView)
    {
        this.correctCol = correctCol;
        this.wrongCol = wrongCol;
        this.normalCol = normalCol;
        this.options = options;
        this.quizView = quizView;
        
        //listener for all options
        foreach (Button localBtn in options)
        {
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }
    
    //Options click
    void OnClick(Button btn)
    {
        //if not answered before
        if (!isAnswered)
        {
            //set answered true
            isAnswered = true;
            //check if answer is correct
            bool isTrueAnswer = quizView.Answer(btn.name);

            //blinking for true and wrong
            BlinkOption(btn.image, isTrueAnswer ? correctCol : wrongCol);
        }
    }
    
    public void Clear()
    {
        foreach (Button localBtn in options)
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
    async void BlinkOption(Image image, Color color)
    {
        Color[] colors = {Color.black, color, Color.black, color};
        for (int i = 0; i < colors.Length; i++)
        {
            ChangeColor(image, colors[i]);
            await Task.Delay(100);
        }
    }

    void ChangeColor(Image image, Color color)
    {
        if (image != null)
        {
            image.color = color;
        }
    }
}
