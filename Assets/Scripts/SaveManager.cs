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

    public static void SaveAllMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            ES2.Save(CoinManager.currentCoins, "savas.txt?tag=coins");
            ES2.Save(ScoreManage.highestScore, "savas.txt?tag=highestScore");
            ES2.Save(StaticContainer.username, "savas.txt?tag=username");
        }
    }

    public static void SaveAllMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ES2.Save(StaticContainer.username, "savas.txt?tag=username");
        }
    }

    void OnApplicationQuit()
    {
        SaveAllMainScene();
        SaveAllMainMenu();
    }
}
