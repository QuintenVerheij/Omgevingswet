using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReadProfile : MonoBehaviour, IPointerClickHandler
{
    MessageWithItem<UserOutput> res;
    public Text username;
    currentUser user;
    AuthorizationToken token;
    AuthorizedAction<int> action;

    private Rect WindowRect = new Rect((Screen.width / 2) - 400, Screen.height / 2 - 600, 800, 450);

    public bool isProfilePicTapped;
    // Start is called before the first frame update
    void Start()
    {
        user = new currentUser();
        token = new AuthorizationToken(user.readToken());
        action = new AuthorizedAction<int>(token, user.readUserId());
        isProfilePicTapped = false;
        StartCoroutine(GetUserInfo());
    }

    IEnumerator GetUserInfo()
    {
        UnityWebRequest net = new UnityWebRequest("localhost:8080/user/read", "POST");
        net.uploadHandler = (UploadHandler)new UploadHandlerRaw(action.toJsonRaw());
        net.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        net.SetRequestHeader("Content-Type", "application/json");
        yield return net.SendWebRequest();
        if (net.isNetworkError || net.isHttpError)
        {
            Debug.Log(net.error);
            username.text = net.error;
        }
        else
        {
            Debug.Log(net.downloadHandler.text);
            res = (MessageWithItem<UserOutput>)MessageWithItem<UserOutput>.fromJson(net.downloadHandler.text);
            //username.text = res.item.username;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isProfilePicTapped)
        {
            if (eventData.position.x < 144 || eventData.position.x > 930 || eventData.position.y < 1115 || eventData.position.y > 1560)
            {
                isProfilePicTapped = false;
            }
        }
    }

    private void OnGUI()
    {
        if (isProfilePicTapped)
        {
            WindowRect = GUI.Window(0, WindowRect, picChoices, "Main Menu");
        }
    }

    private void picChoices(int id)
    {
        GUIStyle buttons = new GUIStyle("button");
        buttons.fontSize = 40;
        if (GUI.Button(new Rect(50, 50, 700, 150), "Camera", buttons))
        {
            Debug.Log("Camera");
        }
        if (GUI.Button(new Rect(50, 250, 700, 150), "Gallery", buttons))
        {
            Debug.Log("Gallery");
        }

    }
}
