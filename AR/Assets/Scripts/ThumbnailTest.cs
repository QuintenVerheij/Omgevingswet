using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailTest : MonoBehaviour
{
    //public ThumbnailUtility controller;
    public RawImage image;
    public float distance = 2;
    public Vector3 orientation;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //var thumbnail = controller.CreateThumbnail(300, 300, distance, Quaternion.Euler(orientation), prefab);
            //image.texture = thumbnail;
        }
    }
}
