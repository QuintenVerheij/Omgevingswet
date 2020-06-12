using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveModel : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderValueDisplay;

    public Toggle toggle;
    public GameObject sliderPanel;

    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { setPanelActive(toggle.GetComponent<Transform>(), !toggle.isOn); });
        slider.onValueChanged.AddListener(delegate { sliderValueChanged(slider.value); });
    }

    void setPanelActive(Transform t, bool input)
    {
        sliderPanel.SetActive(input);
        Transform[] ts = sliderPanel.GetComponentsInChildren<Transform>(true);
        foreach(Transform child in ts)
        {
            child.gameObject.SetActive(input);
        }
    }

    void sliderValueChanged(float input)
    {
        switch (input)
        {
            case 0:
                sliderValueDisplay.text = "100 meter";
                break;
            case 1:
                sliderValueDisplay.text = "500 meter";
                break;
            case 2:
                sliderValueDisplay.text = "1 km";
                break;
            case 3:
                sliderValueDisplay.text = "2 km";
                break;
            case 4:
                sliderValueDisplay.text = "5 km";
                break;
            case 5:
                sliderValueDisplay.text = "10 km";
                break;
            case 6:
                sliderValueDisplay.text = "25 km";
                break;
            default:
                sliderValueDisplay.text = "100 meter";
                break;
        }
    }

    void Update()
    {

    }
}
