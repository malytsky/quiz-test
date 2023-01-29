using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizStartManager : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Text scoreText;
    [SerializeField] private Toggle randomOrderToggle;

    private void Start()
    {
        if (btnStart != null)
        {
            btnStart.onClick.AddListener(OnStartButtonClick);
        }
        if (btnQuit != null)
        {
            btnQuit.onClick.AddListener(OnExitButtonClick);
        }
        scoreText.text = QuizVariables.TextRecord + PlayerPrefs.GetInt(QuizVariables.TextToSaveRecord);
    }

    private void OnStartButtonClick()
    {
        QuizVariables.RandomOrder = randomOrderToggle.isOn;
        SceneManager.LoadScene("GameScene");
    }
    
    private static void OnExitButtonClick()
    {
        Application.Quit();
    }
}