using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

public class OpenARFromPrefab: MonoBehaviour {

    currentUser cu = new currentUser();

    private string j = "";

    public string open(int modelId){
        StartCoroutine(openAR(modelId));
        return j;
    }

    public IEnumerator openAR(int modelId){
        string url = AppStartup.APIURL + ":8080/model/read/";
        if (cu.readUserId() == -1)
        {
            url = AppStartup.APIURL + ":8080/model/public/read/";
        }
        url += modelId;
        Debug.Log(url);
        UnityWebRequest uwr = new UnityWebRequest();
        if(cu.readUserId() == -1){
            uwr = new UnityWebRequest(url, "GET");
            uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        }
        else 
        {
            AuthorizedAction<int> auth = new AuthorizedAction<int>(new AuthorizationToken(cu.readToken()), cu.readUserId());
            uwr = new UnityWebRequest(url, "POST");
            uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw(auth.toJsonRaw());
            uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");
        }

        yield return uwr.SendWebRequest();
        if(uwr.isHttpError || uwr.isNetworkError){
            Debug.Log(uwr.error);
        }else{
            MessageWithItem<ModelOutput> m = MessageWithItem<ModelOutput>.fromJson(uwr.downloadHandler.text);
            if(m.message.successful){
                string json = System.Text.Encoding.UTF8.GetString(m.item.json);
                Debug.Log(json);
                j = json;
            }else{
                Debug.Log(m.message.ToString());
            }
        }
    }
}