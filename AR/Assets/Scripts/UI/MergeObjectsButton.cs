﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MergeObjectsButton : MonoBehaviour
{
    public Camera thumbnailCam;
    public TMP_Text text;
    public Button button;
    public Image image;

    public void Update() {
        var handler = ObjectSelectionHandler.Instance;
        bool visible = handler.CanCombineSelectedModels();
        text.enabled = visible;
        button.enabled = visible;
        image.enabled = visible;
    }
    public void OnPress() {
        var handler = ObjectSelectionHandler.Instance;
        if (handler.CanCombineSelectedModels()) {
            handler.CombineSelectedModels(thumbnailCam);    
        }


    }
}
