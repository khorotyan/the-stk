using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public Text coins;

    public static int currentCoins = 0;
    public static int coinsPerPlacement = 1;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        ManageCoins();
	}

    void ManageCoins()
    {
        coins.text = "Coins  " + currentCoins; 
    }
}
