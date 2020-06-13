using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMarker : MonoBehaviour
{
    private MarkerData _data;
    public GameObject canvas;
    public void SetMarkerData(MarkerData data)
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
        //RawImage rawImage = canvas.gameObject.GetComponentInChildren<RawImage>();
        //rawImage.texture = texture;
    }
}
