using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginRequest : MonoBehaviour
{
    public InputField userName;
    public InputField password;
    public Text responseText;
    public AuthorizationTokenReturn output;
    public Button loginButton;

    public void ClickLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        string url = "localhost:8080/auth/login";

        //data = new LoginDataToJSON();
        //data.mail = userName.text;
        //data.password = password.text;
        //string body = JsonUtility.ToJson(data);
        //byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(body);

        byte[] jsonToSend = new AuthorizationTokenRequest(userName.text, password.text).toJsonRaw();

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Response text if WebRequest gives an error
            responseText.text = www.error;
            Debug.Log(www.error);
        }
        else
        {
            output = AuthorizationTokenReturn.fromJson(www.downloadHandler.text);
            if (output.successful)
            {
                currentUser cu = new currentUser();
                cu.writeUserId(output.userId ?? default(int));
                cu.writeToken(output.token);
                SceneManager.LoadScene(3);
            }
            else
            {
                responseText.text = output.message;
                Debug.Log(output);
            }
        }

    }

}