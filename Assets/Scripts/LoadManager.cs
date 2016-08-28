﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadManager : MonoBehaviour
{

	void Awake ()
    {
        LoadAllMainScene();
	}
	
	void Update ()
    {
	
	}

    public void LoadAllMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (ES2.Exists("savas.txt?tag=coins"))
                CoinManager.currentCoins = ES2.Load<int>("savas.txt?tag=coins");

            if (ES2.Exists("savas.txt?tag=highestScore"))
                ScoreManage.highestScore = ES2.Load<int>("savas.txt?tag=highestScore");
        }
    }

    public void LoadAllMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (ES2.Exists("savas.txt?tag=coins"))
                CoinManager.currentCoins = ES2.Load<int>("savas.txt?tag=coins");

            if (ES2.Exists("savas.txt?tag=highestScore"))
                ScoreManage.highestScore = ES2.Load<int>("savas.txt?tag=highestScore");
        }
    }
}
