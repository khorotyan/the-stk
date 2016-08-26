using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManage : MonoBehaviour
{
    public Text scoreText;

    public static int currentScore = 0;
    public static int scorePerPlacement = 5;

	void Start ()
    {

    }
	
	void Update ()
    {
        ManageScores();

    }

    void ManageScores()
    {
        scoreText.text = "Score  " + currentScore;
    }
}
