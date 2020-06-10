using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginRequest : MonoBehaviour
{
    public InputField userName;
    public InputField password;

    public LoginDataToJSON data;

    public Button loginButton;

    public void ClickLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        string url = "localhost:8080/auth/login";

        data = new LoginDataToJSON();
        data.mail = userName.text;
        data.password = password.text;
        string body = JsonUtility.ToJson(data);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(body);

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(body);
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Login Complete");
        }
        
    }

    public void CheckInputFields()
    {
        loginButton.interactable = (userName.text.Length >= 1 && password.text.Length >= 1);
    }

}