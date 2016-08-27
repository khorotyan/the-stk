using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHighscores : MonoBehaviour
{
    public Text[] names;
    public Text[] scores;
    OnlineHighscores highscoreManager;

	void Start ()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = (i + 1) + ". Retrieving Data ...";
        }

        highscoreManager = GetComponent<OnlineHighscores>();

        StartCoroutine(RefreshHighscores());
    }
	
	public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = (i + 1) + ". ";
            if (highscoreList.Length > i)
            {
                names[i].text += highscoreList[i].username + " ";
                scores[i].text = highscoreList[i].score + "";
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
