using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveManager : MonoBehaviour
{

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SaveAllMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            ES2.Save(CoinManager.currentCoins, "savas.txt?tag=coins");
            ES2.Save(ScoreManage.highestScore, "savas.txt?tag=highestScore");
        }
    }

    public void SaveAllMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ES2.Save(CoinManager.currentCoins, "savas.txt?tag=coins");
            ES2.Save(ScoreManage.highestScore, "savas.txt?tag=highestScore");
        }
    }

    void OnApplicationQuit()
    {
        SaveAllMainScene();
    }
}
