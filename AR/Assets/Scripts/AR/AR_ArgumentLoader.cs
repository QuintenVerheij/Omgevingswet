using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_ArgumentLoader : MonoBehaviour
{
    public Transform placedObjectsFolder;

    int index;
    JSONCombinedModel jsonModel;

    // Start is called before the first frame update
    void Start()
    {
        ARSceneOpener opener = FindObjectOfType<ARSceneOpener>(); //should be initialised in the other scene
        if (!opener) {
            Debug.LogWarning("ARSceneOpener has not been found. Will not initialise model without that component");
            return;
        }

        print("ARSceneOpener has been found.");

        index = opener.GetBuiltInModelIndex();
        jsonModel = opener.GetCustomModel();

        Destroy(opener.gameObject);
    }

    public void PlaceArgumentModel() {
        ObjectCreationHandler handler = ObjectCreationHandler.Instance;

        if (index != -1) {
            Model model = handler.models[index];
            print("Initialising model of built-index argument.");
            var instance = Instantiate(model.gameObject);

            instance.gameObject.SetActive(true);
            instance.transform.localScale = Vector3.Scale(model.transform.lossyScale, placedObjectsFolder.lossyScale);
            instance.transform.position = placedObjectsFolder.position;
            instance.transform.rotation = placedObjectsFolder.rotation;
            instance.transform.parent = placedObjectsFolder;

        }
        else if (jsonModel != null) {
            var instance = JSONModelUtility.JSONModelToCombinedModel(jsonModel, placedObjectsFolder, jsonModel.name); //this function already initialises the model in the scene
            instance.transform.position = placedObjectsFolder.position;

            instance.gameObject.SetActive(true);

        }
        else {
            Debug.LogWarning($"Arguments of ARSceneOpener are invalid. index:{index}, json model: {jsonModel}");
        }
    }
}
