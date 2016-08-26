using UnityEngine;
using System.Collections;

public class LoadManager : MonoBehaviour
{

	void Awake ()
    {
        LoadAll();
	}
	
	void Update ()
    {
	
	}

    public void LoadAll()
    {
        if (ES2.Exists("savas.txt?tag=coins"))  
            CoinManager.currentCoins = ES2.Load<int>("savas.txt?tag=coins");

        if (ES2.Exists("savas.txt?tag=highestScore"))
            ScoreManage.highestScore = ES2.Load<int>("savas.txt?tag=highestScore");
    }
}
