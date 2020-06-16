using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class AppStartup : MonoBehaviour
{
    public static string APIURL;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        //CHANGE THIS TO LOCAL IP OF COMPUTER RUNNING THE BACKEND
        //"http://<LOCALIP>"
        APIURL = "http://192.168.2.1";


        currentUser user = new currentUser();
        string dirpath = Application.persistentDataPath + "/currentUser";
        if(!Directory.Exists(dirpath)){
            Directory.CreateDirectory(dirpath);
        }
        string path = Application.persistentDataPath + "/currentUser" + "/currentUser.txt";
        currentUser.path = path;
        if(!File.Exists(path)){
            using(StreamWriter w = File.CreateText(path))
            {
                w.WriteLine("{\"userId\":-1,\"token\":\"\"}");
            }
            
        }
        
        
        AuthorizationToken token = new AuthorizationToken(user.readToken());
        string url = APIURL + ":8080/auth/whoami";
        UnityWebRequest uwr = new UnityWebRequest(url, "POST");
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(token.toJsonRaw());
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-type", "application/json");
        
        using (uwr)
        {
            uwr.SendWebRequest();
            while (!uwr.isDone && !uwr.isHttpError && !uwr.isNetworkError){} 
            
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Debug.Log(uwr.downloadHandler.text);
                MessageWithItem<AuthorizationWhoAmIResult> message = 
                  (MessageWithItem<AuthorizationWhoAmIResult>) MessageWithItem<AuthorizationWhoAmIResult>.fromJson(uwr.downloadHandler.text);
                if (message.item != null){
                  user.writeUserId(message.item.userId);
                }else{
                    user.writeToken("");
                    user.writeUserId(-1);
                }
            }
        }
    }
}
