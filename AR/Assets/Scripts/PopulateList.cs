using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateList : MonoBehaviour
{
    // Start is called before the first frame update
    public void Populate(List<ModelOutputPreview> models, GameObject prefab)
    {
        GameObject newObj;

        foreach (ModelOutputPreview model in models)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            newObj.transform.SetParent(GetComponent<VerticalLayoutGroup>().transform, false);
            newObj.GetComponent<Canvas>().enabled = true;
            Texture2D newTexture = new Texture2D(650, 550, TextureFormat.ARGB4444, false);
            newTexture.LoadImage(model.preview);
            newTexture.Apply();
            newObj.GetComponentInChildren<ModelIdHolder>().modelId = model.id;
            newObj.GetComponentInChildren<RawImage>().texture = newTexture;
            Debug.Log("LOADED");
            //newObj.GetComponent<ViewOtherModel>().SetModelID(model.id);
        }
    }
}
