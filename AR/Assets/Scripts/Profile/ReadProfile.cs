using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class ReadProfile : MonoBehaviour, IPointerClickHandler
{
    public Text username;
    currentUser user;
    AuthorizationToken token;
    AuthorizedAction<int> action;
    public RawImage PictureFinal;
    Texture2D pic;
    public static int crossedId;
    public Text amtModels;

    public GameObject content;
    public GameObject prefab;

    public GameObject modelisEmpty;
    private Rect ChoiceWindow = new Rect((Screen.width / 2) - 400, Screen.height / 2 - 600, 800, 450);
    private Rect CamWindow = new Rect(0, 0, Screen.width, Screen.height - 150);

    private WebCamTexture webCamTexture;
    public GameObject registerButton;
    public Texture webcamButton;
    public bool isProfilePicTapped;
    private bool showCam;
    // Start is called before the first frame update
    void Start()
    {
        user = new currentUser();
        if (user.readUserId() != -1)
        {
            registerButton.SetActive(false);
        }
        if (crossedId == 0)
        {
            crossedId = user.readUserId();
        }
        Debug.Log("crossedId = " + crossedId + ", readUserId = " + user.readUserId());
        token = new AuthorizationToken(user.readToken());
        action = new AuthorizedAction<int>(token, crossedId);
        pic = new Texture2D(300, 300);
        if (crossedId == user.readUserId())
        {
            StartCoroutine(GetUserInfo());
        }
        else
        {
            StartCoroutine(GetOtherUser());
        }
        StartCoroutine(GetPic());
    }

    IEnumerator GetPic()
    {
        UnityWebRequest uwr = new UnityWebRequest("localhost:8080/user/img/" + user.readUserId(), "GET");
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log("GetPic: " + uwr.error);
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            byte[] bytes = uwr.downloadHandler.data;
            pic.LoadImage(bytes);
            pic.Apply();

            PictureFinal.texture = pic;

            //username.text = res.item.username;
        }
    }
    IEnumerator GetOtherUser()
    {
        string url = "localhost:8080/user/other/read/" + crossedId;
        UnityWebRequest net = new UnityWebRequest(url, "GET");
        net.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return net.SendWebRequest();
        if (net.isNetworkError || net.isHttpError)
        {
            Debug.Log("Other User: " + net.error);
            username.text = net.error;
        }
        else
        {
            Debug.Log(net.downloadHandler.text);
            MessageWithItem<UserOutputPublic> usernameres = MessageWithItem<UserOutputPublic>.fromJson(net.downloadHandler.text);
            if (usernameres.message.successful)
            {
                amtModels.text = usernameres.item.models.Count + amtModels.text.Substring(1);
                if (usernameres.item.models.Count > 0)
                {
                    modelisEmpty.SetActive(false);
                    PopulateModels(usernameres.item.models);
                }
                username.text = usernameres.item.username;
            }
            else
            {
                username.text = "[Niet ingelogd]";
            }
        }
    }
    IEnumerator GetUserInfo()
    {
        string url = "localhost:8080/user/read";
        UnityWebRequest net = new UnityWebRequest(url, "POST");
        net.uploadHandler = (UploadHandler)new UploadHandlerRaw(action.toJsonRaw());
        net.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        net.SetRequestHeader("Content-Type", "application/json");
        yield return net.SendWebRequest();
        if (net.isNetworkError || net.isHttpError)
        {
            Debug.Log("Self User: " + net.error);
            username.text = net.error;
        }
        else
        {
            Debug.Log(net.downloadHandler.text);
            MessageWithItem<UserOutput> usernameres = MessageWithItem<UserOutput>.fromJson(net.downloadHandler.text);
            if (usernameres.message.successful)
            {
                amtModels.text = usernameres.item.models.Count + amtModels.text.Substring(1);
                if (usernameres.item.models.Count > 0)
                {
                    modelisEmpty.SetActive(false);
                    PopulateModels(usernameres.item.models);
                }
                username.text = usernameres.item.username;
            }
            else
            {
                username.text = "[Niet ingelogd]";
            }
        }
    }

    void PopulateModels(List<ModelOutputPreview> models)
    {
        content.GetComponent<PopulateList>().Populate(models, prefab);
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
            WebCamDevice[] devices = WebCamTexture.devices;
            webCamTexture = new WebCamTexture(devices[0].name);
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
        webCamTexture.Stop();
        StartCoroutine(UploadPhoto());
        //TODO: Upload image to backend, reload profile page
    }
    IEnumerator UploadPhoto()
    {
        // byte[] boundary = UnityWebRequest.GenerateBoundary();
        // UnityWebRequest www = new UnityWebRequest("localhost:8080/user/img/upload?auth=" + user.readToken() + "&input=" + user.readUserId(), "POST");
        // List<IMultipartFormSection> requestData = new List<IMultipartFormSection>();
        // requestData.Add(new MultipartFormDataSection("auth=" + user.readToken() + "&input=" + user.readUserId()));
        // requestData.Add(new MultipartFormFileSection("file", File.ReadAllBytes("Assets/Resources/" + user.readUserId() + "_Profile.png"), "upload.png", "image/png"));
        // byte[] formSections = UnityWebRequest.SerializeFormSections(requestData, boundary);
        // www.uploadHandler = (UploadHandler)new UploadHandlerRaw(formSections);
        // www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        // www.uploadHandler.contentType = "multipart/form-data; boundary=\"" + System.Text.Encoding.UTF8.GetString(boundary) + "\"";
        // www.SetRequestHeader("Content-Type", "multipart/form-data");
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", File.ReadAllBytes("Assets/Resources/" + user.readUserId() + "_Profile.png"), "upload.png", "image/png");
        UnityWebRequest www = UnityWebRequest.Post("localhost:8080/user/img/upload?auth=" + user.readToken() + "&input=" + user.readUserId(), form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Message m = Message.fromJson(www.downloadHandler.text);
            Debug.Log(m.ToString());
            new LoadSceneWithUserId().SceneLoader(user.readUserId());
        }
    }
}