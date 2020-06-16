using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ViewOtherModel : MonoBehaviour
{
    private int _modelID;
    void Start()
    {
        _modelID = 0;
    }

    public void SetModelID(int id)
    {
        _modelID = id;
    }
    public void LoadModel()
    {
        StartCoroutine(LoadOtherModel());
    }
    IEnumerator LoadOtherModel()
    {
        string url;
        currentUser cu = new currentUser();
        UnityWebRequest www;

        byte[] jsonToSend = new AuthorizedAction<int>(new AuthorizationToken(cu.readToken()), _modelID).toJsonRaw();
        url = "localhost:8080/model/read/";
        www = new UnityWebRequest(url + cu.readUserId().ToString(), "POST");
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Response text if WebRequest gives an error
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data);
        }
    }
}
