using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour
{

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void SaveAll()
    {
        ES2.Save(CoinManager.currentCoins, "savas.txt?tag=coins");
        ES2.Save(ScoreManage.highestScore, "savas.txt?tag=highestScore");
    }

    void OnApplicationQuit()
    {
        SaveAll();
    }
}
