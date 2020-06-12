using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReadProfile : MonoBehaviour
{
    MessageWithItem<UserOutput> res;
    public Text username;
    string[] authJson = {
        "auth", {
            "token","0cb6ebb9511aa521864d39542d0cce4e4a29886c85cf926afd016f147f6a877675aaef0fab332d0ca834cac07ce2287631726d4fd968485f06b384c039448b85"
        },
        "input",0
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (GetUserInfo());
    }

    IEnumerator GetUserInfo() {
        UnityWebRequest net = new UnityWebRequest("localhost:8080/user/read", "POST");
        net.uploadHandler = (UploadHandler) new UploadHandlerRaw(authJson);
        net.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        net.SetRequestHeader("Content-Type", "application/json");
        yield return net.SendWebRequest();
        if (net.isNetworkError || net.isHttpError) {
            Debug.Log(net.error);
            username.text = net.error;
        } else {
            Debug.Log(net.downloadHandler.text);
            res = (MessageWithItem<UserOutput>) MessageWithItem<UserOutput>.fromJson(net.downloadHandler.text);
            username.text = res.item.username;
        }
    }
}
