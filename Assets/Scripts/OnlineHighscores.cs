using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OnlineHighscores : MonoBehaviour
{
    public static string privateCode = "7z9DM2tx5UWnGxra7FyyRgcynr-SCcQ0qChicF76v3Jw";
    public static string publicCode = "57c13d0d8af603118c7bceb4";
    public static string webURL = "http://dreamlo.com/lb/";

    public static Highscore[] highscoreList = new Highscore[0];
    public static bool canCommunicateWithServer = false;
    static OnlineHighscores instance;
    DisplayHighscores displayHighscores;

    void Awake()
    {
        instance = this;

        if (SceneManager.GetActiveScene().name == "LeaderboardScene")
            displayHighscores = GetComponent<DisplayHighscores>();
    }

    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    // Coroutine is used because uploading to the database is not instantanious
    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Success Uploading");
            DownloadHighscores();
        }
        else
            print("Error Uploading " + www.error);
    }

    public static void DeleteHighscore(string username)
    {
        instance.StartCoroutine(instance.DeleteHighscoresFromDatabase(username));
    }

    // Coroutine is used because uploading to the database is not instantanious
    IEnumerator DeleteHighscoresFromDatabase(string username)
    {
        WWW www = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(username));
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Success Deleting");
            DownloadHighscores();
        }
        else
            print("Error Deleting " + www.error);
    }

    public static void DownloadHighscores()
    {
        instance.StartCoroutine(instance.DownloadHighscoreFromDatabase());
    }

    IEnumerator DownloadHighscoreFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www; // Wait for www to finish getting the scores

        if (string.IsNullOrEmpty(www.error))
        {
            canCommunicateWithServer = true;
            FormatHighscores(www.text);
            if (SceneManager.GetActiveScene().name == "LeaderboardScene")
                displayHighscores.OnHighscoresDownloaded(highscoreList);
        }
        else
        {
            canCommunicateWithServer = false;
            print("Error Downloading " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoreList[i] = new Highscore(username, score);
            print(highscoreList[i].username + ": " + highscoreList[i].score);
        }
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string username, int score)
    {
        this.username = username;
        this.score = score;
    }  
}