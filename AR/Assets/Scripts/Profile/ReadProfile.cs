using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class ReadProfile : MonoBehaviour, IPointerClickHandler
{
    MessageWithItem<UserOutput> res;
    public Text username;
    currentUser user;
    AuthorizationToken token;
    AuthorizedAction<int> action;
    private Rect ChoiceWindow = new Rect((Screen.width / 2) - 400, Screen.height / 2 - 600, 800, 450);
    private Rect CamWindow = new Rect(0, 0, Screen.width, Screen.height);

    private WebCamTexture webCamTexture;

    public Texture webcamButton;
    public bool isProfilePicTapped;
    private bool showCam;
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
            ChoiceWindow = GUI.Window(0, ChoiceWindow, picChoices, "Choose a source");
        }
        if (showCam)
        {
            CamWindow = GUI.Window(1, CamWindow, CamTexture, "Camera");
        }
    }

    private void picChoices(int id)
    {
        GUIStyle buttons = new GUIStyle("button");
        buttons.fontSize = 40;
        if (GUI.Button(new Rect(50, 50, 700, 150), "Camera", buttons))
        {
            Debug.Log("Camera");
            webCamTexture = new WebCamTexture();
            GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
            webCamTexture.Play();

            showCam = true;
            isProfilePicTapped = false;
        }
        if (GUI.Button(new Rect(50, 250, 700, 150), "Gallery", buttons))
        {
            Debug.Log("Gallery");
        }

    }

    private void CamTexture(int id)
    {
        GUI.DrawTexture(new Rect(0, 0, CamWindow.width, CamWindow.height), webCamTexture, ScaleMode.ScaleToFit);
        GUIStyle buttons = new GUIStyle("button");
        buttons.fontSize = 40;
        if (GUI.Button(new Rect((CamWindow.width / 2 - 100), CamWindow.height - 200, 200, 200), webcamButton, buttons))
        {
            StartCoroutine(TakePhoto());
        }
    }

    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes("Assets/Resources/" + user.readUserId() + "_Profile.png", bytes);
        showCam = false;
        //TODO: Upload image to backend, reload profile page
    }
}
