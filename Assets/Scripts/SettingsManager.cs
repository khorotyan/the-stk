using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public InputField nameInputField;
    public Text networkErrorText;

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
        
        if (OnlineHighscores.canCommunicateWithServer == true && networkErrorText.IsActive())
        {
            StartCoroutine(WaitUntillInternetIsOnOrOFF());
            nameInputField.gameObject.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
            networkErrorText.enabled = false;
            nameInputField.enabled = true;          
        }
        else if (OnlineHighscores.canCommunicateWithServer == false && !networkErrorText.IsActive() && settingsPanel.activeSelf == true)
        {
            StartCoroutine(WaitUntillInternetIsOnOrOFF());
            nameInputField.gameObject.GetComponent<Image>().color = new Color32(33, 32, 32, 255);
            networkErrorText.enabled = true;
            nameInputField.enabled = false;
        }

        if (deletedHighscores == true)
        {
            for (int i = 0; i < OnlineHighscores.highscoreList.Length; i++)
            {
                if (OnlineHighscores.highscoreList[i].username == prevUsername)
                {
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

            StaticContainer.username = nameInputField.text;
            SaveManager.SaveAllMainMenu();

            addedHighscores = false;
        }
    }

    public void OpenCloseSettings()
    {
        // Get the previous username while opening the settings panel
        if (settingsPanel.activeSelf == false && nameInputField.text != null)
        {
            settingsPanel.SetActive(true);
            prevUsername = nameInputField.text;
        }

        // Wait a little to make sure that the coroutines were stopped and the data is uploaded to the server untill
        //      closing the settings panel
        else if (settingsPanel.activeSelf == true && nameInputField.text != null)
        {
            StartCoroutine(WaitUntillClose());
        }
    }

    // Whenever the new name is inputed, delete the score with the previous name from the server
    //      and add the same score with the new name
    public void OnFinishedEnteringName()
    {
        StartCoroutine(RefreshHighscoresForD());
    }

    IEnumerator RefreshHighscoresForD()
    {
        OnlineHighscores.DownloadHighscores();
        yield return new WaitForSeconds(0.02f);
        deletedHighscores = true;
    }

    IEnumerator RefreshHighscoresForA()
    {
        OnlineHighscores.DownloadHighscores();
        yield return new WaitForSeconds(0.02f);
        addedHighscores = true;
    }

    IEnumerator WaitUntillClose()
    {
        yield return new WaitForSeconds(0.5f);
        settingsPanel.SetActive(false);
    }

    IEnumerator WaitUntillInternetIsOnOrOFF()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            OnlineHighscores.DownloadHighscores();
        }        
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
