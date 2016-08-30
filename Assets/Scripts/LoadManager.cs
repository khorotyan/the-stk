using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{
    public InputField usernameInputField;

	void Awake ()
    {
        GeneralLoad();
        LoadAllMainScene();
        LoadAllMainMenu();
	}
	
	void Update ()
    {
	     
	}

    public void GeneralLoad()
    {
        if (ES2.Exists("savas.txt?tag=highestScore"))
            ScoreManage.highestScore = ES2.Load<int>("savas.txt?tag=highestScore");
    }

    public void LoadAllMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (ES2.Exists("savas.txt?tag=coins"))
                CoinManager.currentCoins = ES2.Load<int>("savas.txt?tag=coins");
   
        }
    }

    public void LoadAllMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (ES2.Exists("savas.txt?tag=username"))
            {
                StaticContainer.username = ES2.Load<string>("savas.txt?tag=username");
                usernameInputField.text = StaticContainer.username;
            }
        }
    }
}
