using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSceneUI : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject modelPageUI;
    public static ARSceneUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        mainUI.SetActive(true);
        modelPageUI.SetActive(false);
    }
    public void Swap() {
        mainUI.SetActive(mainUI.activeSelf == false);
        modelPageUI.SetActive(modelPageUI.activeSelf == false);
    }
}
