using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelSlot : MonoBehaviour
{
    public TMP_Text text;
    public Button button;
    public RawImage image;

    private int modelIndex = -1;
    private RenderTexture tempRenderTexture;

    public void AttachModel(int modelIndex, Camera thumbnailCamera) {
        button.onClick.RemoveAllListeners();
        this.modelIndex = modelIndex;

        if (modelIndex >= 0) {
            Model model = ObjectCreationHandler.Instance.models[modelIndex];
            text.text = model.name;
            button.onClick.AddListener(Select);
            tempRenderTexture = ThumbnailManager.Instance.CreateThumbnail(thumbnailCamera, 300, 300, model.thumbnailDistance, Quaternion.Euler(model.thumbnailOrientation), model.gameObject);
            image.texture = tempRenderTexture;

        }
        else {
            text.text = "-";
        }
    }

    public void Select() {
        if(modelIndex >= 0) {
            ObjectCreationHandler.Instance.currentIndex = modelIndex;
            ARSceneUI.Instance.Swap();
            print("selected index: " + modelIndex);
        }
    }
}
