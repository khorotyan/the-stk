using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManage : MonoBehaviour
{
    public Text scoreText;

    public static int currentScore = 0;
    public static int scorePerPlacement = 5;
    public static int highestScore = 0;

	void Start ()
    {

    }
	
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
            ManageScores();
    }

    void ManageScores()
    {
        scoreText.text = "Score  " + currentScore;
    }
}
