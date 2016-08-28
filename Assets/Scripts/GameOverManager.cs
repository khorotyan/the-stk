using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject mainPanel;
    public Text highestScore;
    public Text currentScore;
    public InputField nameInputField;

    private bool isGameOver = false;

	void Start ()
    {
        nameInputField.characterLimit = 15;
	}
	
	void Update ()
    {
	
	}

    public void ManageGameOver()
    {
        gameOverPanel.SetActive(!gameOverPanel.activeSelf);
        mainPanel.SetActive(!mainPanel.activeSelf);

        if (mainPanel.activeSelf == true)
            isGameOver = false;
        else
            isGameOver = true;

        if (isGameOver == true)
        {         
            IfGameOver();
            UploadScores();
        }
        else
            IfNotGameOver();
    }

    void IfGameOver()
    {
        currentScore.text = "Score:  " + ScoreManage.currentScore;

        if (ScoreManage.highestScore < ScoreManage.currentScore)
        {
            ScoreManage.highestScore = ScoreManage.currentScore;
            highestScore.text = "New Highscore:  " + ScoreManage.highestScore;
        }
        else
            highestScore.text = "Highest Score:  " + ScoreManage.highestScore;

        Time.timeScale = 0;       
    }

    void IfNotGameOver()
    {
        Time.timeScale = 1;
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    
    public void UploadScores()
    {
        if (nameInputField.text == "" || nameInputField.text == null)
            OnlineHighscores.AddNewHighscore("Player", ScoreManage.currentScore);
        else
            OnlineHighscores.AddNewHighscore(nameInputField.text, ScoreManage.currentScore);
    }
}
