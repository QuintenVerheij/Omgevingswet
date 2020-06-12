using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppStartup : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        currentUser user = new currentUser();
        AuthorizationToken token = new AuthorizationToken(user.readToken());
        string url = "localhost:8080/auth/whoami";
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
