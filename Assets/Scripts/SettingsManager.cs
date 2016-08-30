using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public InputField nameInputField;

    private string prevUsername = "Player";
    private bool deletedHighscores = false;
    private bool addedHighscores = false;

    void Start ()
    {
        nameInputField.characterLimit = 15;
        nameInputField.contentType = InputField.ContentType.Alphanumeric;
        nameInputField.caretBlinkRate = 1;
    }

	void Update ()
    {
        if (deletedHighscores == true)
        {
            Debug.Log(2);
            Debug.Log(" - " + OnlineHighscores.highscoreList.Length);

            for (int i = 0; i < OnlineHighscores.highscoreList.Length; i++)
            {
                Debug.Log(OnlineHighscores.highscoreList[i].username);
                if (OnlineHighscores.highscoreList[i].username == prevUsername)
                {
                    Debug.Log(3);
                    OnlineHighscores.DeleteHighscore(OnlineHighscores.highscoreList[i].username);
                }
            }

            StartCoroutine(RefreshHighscoresForA());
            deletedHighscores = false;
        }

        if (addedHighscores == true)
        {
            OnlineHighscores.AddNewHighscore(nameInputField.text, ScoreManage.highestScore);
            OnlineHighscores.DownloadHighscores();
            addedHighscores = false;
        }
    }

    public void OpenCloseSettings()
    {
        // Get the previous username while opening the settings panel
        if (settingsPanel.activeSelf == false && nameInputField.text != "" && nameInputField.text != null)
        {
            prevUsername = nameInputField.text;
        }

        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    // Whenever the new name is inputed, delete the score with the previous name from the server
    //      and add the same score with the new name
    public void OnFinishedEnteringName()
    {
        StartCoroutine(RefreshHighscoresForD());

        StaticContainer.username = nameInputField.text;

        SaveManager.SaveAllMainMenu();
    }

    IEnumerator RefreshHighscoresForD()
    {
        OnlineHighscores.DownloadHighscores();
        yield return new WaitForSeconds(0.5f);
        deletedHighscores = true;
        Debug.Log(1);
    }

    IEnumerator RefreshHighscoresForA()
    {
        OnlineHighscores.DownloadHighscores();
        yield return new WaitForSeconds(0.2f);
        addedHighscores = true;
    }

    void OnApplicationQuit()
    {
        if (settingsPanel.activeSelf == true)
        {
            OpenCloseSettings();
            OpenCloseSettings();
        }
    }
}
