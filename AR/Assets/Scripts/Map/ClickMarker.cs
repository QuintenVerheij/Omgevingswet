using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Drawing;

public class ClickMarker : MonoBehaviour
{
    private ModelOutputPreview _data;
    private GameObject _popUpMessage;
    public void SetMarkerData(ModelOutputPreview data)
    {
        _data = data;
    }
    public ModelOutputPreview getMarkerData() { return _data; }
    // bool showingPopup = false;
    // public void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         foreach (Touch touch in Input.touches)
    //         {
    //             int id = touch.fingerId;
    //             if (!this.showingPopup && EventSystem.current.IsPointerOverGameObject(id))
    //             {
    //                 showPopup();
    //                 this.showingPopup = true;
    //             }
    //         }
    //     }
    // }

    // public void onClosePopup()
    // {
    //     this.showingPopup = false;
    // }

    public void onMouseUp()
    {
        _popUpMessage = GameObject.Find("PopUpMessage");
        _popUpMessage.GetComponent<Canvas>().enabled = true;
        Debug.Log(_data.id);
        Debug.Log(_data.createdAt);

        //Load in image from backend
        //set image of image child from canvas to image:
        Texture2D newTexture = new Texture2D(650, 550, TextureFormat.ARGB4444, false);
        newTexture.LoadImage(_data.preview);
        newTexture.Apply();
        _popUpMessage.GetComponentInChildren<RawImage>().texture = newTexture;
        Debug.Log(_data);
        _popUpMessage.GetComponent<ViewOtherModel>().SetModelID(_data.id);
        _popUpMessage.GetComponent<ClickViewProfile>().SetUserID(_data.userid);

    }
    public void OnTouchUp()
    {
        _popUpMessage = GameObject.Find("PopUpMessage");
        _popUpMessage.GetComponent<Canvas>().enabled = true;
        Debug.Log(_data.id);
        Debug.Log(_data.createdAt);

        //Load in image from backend
        //set image of image child from canvas to image:
        Texture2D newTexture = new Texture2D(650, 550, TextureFormat.ARGB4444, false);
        newTexture.LoadImage(_data.preview);
        newTexture.Apply();
        _popUpMessage.GetComponentInChildren<RawImage>().texture = newTexture;
        Debug.Log(_data);
        _popUpMessage.GetComponent<ViewOtherModel>().SetModelID(_data.id);
        _popUpMessage.GetComponent<ClickViewProfile>().SetUserID(_data.userid);

    }

    public void showPopup()
    {
        _popUpMessage = GameObject.Find("PopUpMessage");
        _popUpMessage.GetComponent<Canvas>().enabled = true;
        Debug.Log(_data.id);
        Debug.Log(_data.createdAt);

        //Load in image from backend
        //set image of image child from canvas to image:
        Texture2D newTexture = new Texture2D(650, 550, TextureFormat.ARGB4444, false);
        newTexture.LoadImage(_data.preview);
        newTexture.Apply();
        _popUpMessage.GetComponentInChildren<RawImage>().texture = newTexture;
        Debug.Log("LOADED");
        _popUpMessage.GetComponent<ViewOtherModel>().SetModelID(_data.id);
    }


}
