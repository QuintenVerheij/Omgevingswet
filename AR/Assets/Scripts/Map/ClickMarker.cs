using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Drawing;

public class ClickMarker : MonoBehaviour
{
    private ModelOutputPreview _data;
    public GameObject canvas;
    public void SetMarkerData(ModelOutputPreview data)
    {
        _data = data;
    }
    public void OnMouseUp()
    {
        GameObject.Find("PopUpMessage").GetComponent<Canvas>().enabled = true;
        Debug.Log(_data.id);
        Debug.Log(_data.createdAt);

        //Load in image from backend
        //set image of image child from canvas to image:
        Texture2D newTexture = new Texture2D(650, 550, TextureFormat.ARGB4444, false);
        newTexture.LoadImage(_data.preview);
        newTexture.Apply();
        GameObject.Find("PopUpMessage").GetComponentInChildren<RawImage>().texture = newTexture;
        Debug.Log("LOADED");
    }
}
