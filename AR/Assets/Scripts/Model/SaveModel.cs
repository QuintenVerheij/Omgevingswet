using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.IO;
public class SaveModel : MonoBehaviour
{
    public double latitude;

    public double longitude;

    public static string modelPath;
    public static string previewPath;
    public Slider slider;
    public Text sliderValueDisplay;
    private int distValue = 100;

    public Toggle toggle;
    public GameObject sliderPanel;

    public Button submit;

    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { setPanelActive(toggle.GetComponent<Transform>(), !toggle.isOn); });
        slider.onValueChanged.AddListener(delegate { sliderValueChanged(slider.value); });
        submit.onClick.AddListener(delegate { StartCoroutine(sendModelMetaData()); });
        

    }

    void setPanelActive(Transform t, bool input)
    {
        sliderPanel.SetActive(input);
        Transform[] ts = sliderPanel.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in ts)
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
                distValue = 100;
                break;
            case 1:
                sliderValueDisplay.text = "500 meter";
                distValue = 500;
                break;
            case 2:
                sliderValueDisplay.text = "1 km";
                distValue = 1000;
                break;
            case 3:
                sliderValueDisplay.text = "2 km";
                distValue = 2000;
                break;
            case 4:
                sliderValueDisplay.text = "5 km";
                distValue = 5000;
                break;
            case 5:
                sliderValueDisplay.text = "10 km";
                distValue = 10000;
                break;
            case 6:
                sliderValueDisplay.text = "25 km";
                distValue = 25000;
                break;
            default:
                sliderValueDisplay.text = "100 meter";
                distValue = 100;
                break;
        }
    }

    void Update()
    {

    }

    private IEnumerator sendModelMetaData()
    {
        currentUser user = new currentUser();
        ModelCreateInput model = new ModelCreateInput
        (
            user.readToken(),
            user.readUserId(),
            toggle.isOn,
            distValue,
            longitude,
            latitude,
            DateTime.Now.ToString()
        );
        Debug.Log(model.ToString());
        string url = "localhost:8080/model/create";
        UnityWebRequest uwr = new UnityWebRequest(url, "POST");
        uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(model.toJsonRaw());
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }else{
            MessageWithItem<int> m = MessageWithItem<int>.fromJson(uwr.downloadHandler.text);
            if (m.message.successful){
                StartCoroutine(sendModelFileData(m.item));
            }else{
                Debug.Log(m.ToString());
            }
        }
    }

    private IEnumerator sendModelFileData(int targetId){
        //TODO: Remove
        previewPath = "Assets/Resources/3_Profile.png";
        modelPath = "Assets/Resources/example.json";
        //
        currentUser user = new currentUser();
        WWWForm form = new WWWForm();
        form.AddBinaryData("modelJson", File.ReadAllBytes(modelPath), "upload.json");
        form.AddBinaryData("preview", File.ReadAllBytes(previewPath), "upload.png", "image/png");
        UnityWebRequest www = UnityWebRequest.Post("localhost:8080/model/create/files?token=" + user.readToken() + 
                                                    "&userId=" + user.readUserId() + 
                                                    "&modelId=" + targetId , form);
        www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }else{
            Message m = Message.fromJson(www.downloadHandler.text);
            if (m.successful){
                Debug.Log("WOO");
            }else{
                Debug.Log(m.ToString());
            }
        }
    }
}
