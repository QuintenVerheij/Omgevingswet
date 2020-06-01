using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputModeButton : MonoBehaviour
{
    private Text buttonText;
    private void Start() {
        buttonText = GetComponentInChildren<Text>();
        DisplayCurrentMode();
    }
    public void DisplayCurrentMode() {
        string mode = ARInputState.Instance.CurrentMode.ToString();
        buttonText.text = "Mode:\n" + mode;
    }
}
