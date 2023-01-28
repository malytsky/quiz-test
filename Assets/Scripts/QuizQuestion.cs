using UnityEngine.UI;

public class QuizQuestion
{
    public void Init(Question question, Text questionInfoText, Image questionImg)
    {
        //check for questionType
        switch (question.questionType)
        {
            case QuestionType.Text:
                questionImg.transform.parent.gameObject.SetActive(false);   //deactivate image holder
                break;
            case QuestionType.Image:
                questionImg.transform.parent.gameObject.SetActive(true);    //activate image holder
                questionImg.transform.gameObject.SetActive(true);           //activate questionImg

                questionImg.sprite = question.questionImage;                //set the image sprite
                break;
        }
        questionInfoText.text = question.questionInfo;                      //set the question text
    }
}
