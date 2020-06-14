using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReadProfile : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        user = new currentUser();
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
}