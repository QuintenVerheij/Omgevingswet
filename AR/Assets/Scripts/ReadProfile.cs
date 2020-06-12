using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReadProfile : MonoBehaviour
{
    MessageWithItem<UserOutput> res;
    public Text username;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (GetUserInfo());
    }

    IEnumerator GetUserInfo() {
        UnityWebRequest net = UnityWebRequest.Get("localhost:8080/user/read");
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
