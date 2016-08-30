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

    private bool isGameOver = false;

	void Start ()
    {

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
            SaveManager.SaveAllMainScene();
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
        OnlineHighscores.AddNewHighscore(StaticContainer.username, ScoreManage.currentScore);
    }
}
