using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridModeButton : MonoBehaviour {
    public TMP_Text buttonText;

    private void Start() {
        DisplayCurrentMode();
    }

    private void OnEnable() {
        if (ObjectSelectionHandler.Instance) {
            DisplayCurrentMode();
        }
    }

    public void DisplayCurrentMode() {
        var mode = ObjectSelectionHandler.Instance.CurrentGridMode;
        buttonText.text = mode.ToString();
    }
    public void OnPress() {
        ObjectSelectionHandler.Instance.NextGridMode();
        DisplayCurrentMode();
    }
}
