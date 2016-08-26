using UnityEngine;
using UnityEngine.UI;
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
            IfGameOver();
        else
            IfNotGameOver();
    }

    void IfGameOver()
    {
        

        Time.timeScale = 0;
        currentScore.text = "Score:  " + ScoreManage.currentScore;
    }

    void IfNotGameOver()
    {
        Time.timeScale = 1;
    }
}
