using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMarker : MonoBehaviour
{

    public GameObject canvas;
    public void OnMouseUp()
    {
        GameObject.Find("PopUpMessage").GetComponent<Canvas>().enabled = true;

    //Load in image from backend
    //set image of image child from canvas to image:
    //RawImage rawImage = canvas.gameObject.GetComponentInChildren<RawImage>();
    //rawImage.texture = texture;
}
}
