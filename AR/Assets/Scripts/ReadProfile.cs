using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReadProfile : MonoBehaviour
{
    MessageWithItem<UserOutput> usernameres;
    public Text username;
    currentUser user;
    AuthorizationToken token;
    AuthorizedAction<int> action;
    RawImage PictureFinal;
    Texture2D profilePic;
    // Start is called before the first frame update
    void Start()
    {
        user = new currentUser();
        token = new AuthorizationToken(user.readToken());
        action = new AuthorizedAction<int>(token, user.readUserId());
        profilePic = new Texture2D(300, 300);
        StartCoroutine (GetUserInfo());
        StartCoroutine (GetProfilePic(profilePic));
    }

    IEnumerator GetProfilePic(Texture2D texture) {
        UnityWebRequest ppic = new UnityWebRequest("localhost:8080/user/img/" + user.readUserId(), "GET");
        ppic.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        yield return ppic.SendWebRequest();
        if (ppic.isNetworkError || ppic.isHttpError) {
            Debug.Log(ppic.error);
        } else {
            Debug.Log(ppic.downloadHandler.text);
            byte[] profileres = System.Text.Encoding.UTF8.GetBytes(ppic.downloadHandler.text);
            texture.LoadRawTextureData(profileres);
            texture.Apply();
            texture.EncodeToJPG();
            PictureFinal.texture = texture;
        }
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
            usernameres = (MessageWithItem<UserOutput>) MessageWithItem<UserOutput>.fromJson(net.downloadHandler.text);
            if(usernameres.message.successful) {
                username.text = usernameres.item.username;
            } else {
                username.text = "[Niet ingelogd]";
            }
        }
    }
}
