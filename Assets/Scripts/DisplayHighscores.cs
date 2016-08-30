using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHighscores : MonoBehaviour
{   
    public GameObject scorePlaceholder;
    public GameObject scPlcParent;

    private GameObject[] scoresNNamesParent = new GameObject[100];

    void Start ()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject tempPlScores = Instantiate(scorePlaceholder, scPlcParent.transform) as GameObject;
            tempPlScores.transform.localScale = Vector3.one;

            scoresNNamesParent[i] = tempPlScores;

            tempPlScores.transform.GetChild(0).GetComponent<Text>().text = (i + 1) + ". Retrieving Data ...";
        }

        StartCoroutine(RefreshHighscores());
    }
	
	public void OnHighscoresDownloaded( Highscore[] highscoreList)
    {
        for (int i = 0; i < 100; i++)
        {
            scoresNNamesParent[i].transform.GetChild(0).GetComponent<Text>().text = (i + 1) + ".  ";

            if (highscoreList.Length > i)
            {
                scoresNNamesParent[i].transform.GetChild(0).GetComponent<Text>().text += highscoreList[i].username + " ";
                scoresNNamesParent[i].transform.GetChild(1).GetComponent<Text>().text = highscoreList[i].score + "";
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            OnlineHighscores.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
