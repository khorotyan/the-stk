using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    public Text comboText;
    public Text multiplierText;
    public static Slider comboSlider;

    public static int currentCombo = 0;
    public static int currentMultiplier = 1;
    public static int pointNCoinMultStarter = 2;
    public static float comboTimerLength = 10f;

	void Awake ()
    {
        comboSlider = GameObject.Find("ComboSlider").GetComponent<Slider>();
        comboSlider.maxValue = comboTimerLength;
        
	}
	
	void Update ()
    {
        ManageComboScript();
        GetPointNCoinMult();
	}

    // Sets the combo and manages the slider
    void ManageComboScript()
    {
        comboText.text = currentCombo.ToString();

        comboSlider.value -= Time.deltaTime;

        if (comboSlider.value == 0)
        {
            currentMultiplier = 1;
            currentCombo = 0;
        }
    }

    // Gets the multiplier value from the currentCombo
    void GetPointNCoinMult()
    {
        if (currentCombo < 5)
            multiplierText.text = "";

        if (currentCombo != 0 && currentCombo % 5 == 0)
        {
            currentMultiplier = currentCombo / 5 + 1;
            multiplierText.text = "x" + currentMultiplier + " Points";
        }
    }

}
