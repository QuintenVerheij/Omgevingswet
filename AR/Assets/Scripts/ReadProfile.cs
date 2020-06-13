using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReadProfile : MonoBehaviour
{
    MessageWithItem<UserOutput> res;
    public Text username;
    currentUser user;
    AuthorizationToken token;
    AuthorizedAction<int> action;
    // Start is called before the first frame update
    void Start()
    {
        user = new currentUser();
        token = new AuthorizationToken(user.readToken());
        action = new AuthorizedAction<int>(token, user.readUserId());
        StartCoroutine (GetUserInfo());
    }

    IEnumerator GetUserInfo() {
        UnityWebRequest net = new UnityWebRequest("localhost:8080/user/read", "POST");
        net.uploadHandler = (UploadHandler) new UploadHandlerRaw(action.toJsonRaw());
        net.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        net.SetRequestHeader("Content-Type", "application/json");
        yield return net.SendWebRequest();
        if (net.isNetworkError || net.isHttpError) {
            Debug.Log(net.error);
            username.text = net.error;
        } else {
            Debug.Log(net.downloadHandler.text);
            res = (MessageWithItem<UserOutput>) MessageWithItem<UserOutput>.fromJson(net.downloadHandler.text);
            if(res.message.successful) {
                username.text = res.item.username;
            } else {
                username.text = "[Niet ingelogd]";
            }
        }
    }
}
