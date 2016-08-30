using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuConfig : MonoBehaviour
{
	
	void Update ()
    {
	
	}

    public void OnStartClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnHighScoresClick()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }
}
