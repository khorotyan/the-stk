using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public InputField nameInputField;

	void Start ()
    {
        nameInputField.characterLimit = 15;
    }

	void Update ()
    {
        
	}

    public void OpenCloseSettings()
    {
        if (settingsPanel.activeSelf == true && nameInputField.text != "" && nameInputField.text != null)
        {
            StaticContainer.username = nameInputField.text;
            SaveManager.SaveAllMainMenu();
        }

        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
