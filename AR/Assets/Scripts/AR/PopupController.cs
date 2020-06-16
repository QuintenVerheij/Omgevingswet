using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public ModelIdHolder modelId;
    public Button viewModel;

    public OpenARFromPrefab prefab;

    public ARSceneOpener opener;

    void Start() 
    {
        viewModel.onClick.AddListener(delegate {opener.OpenARScene(prefab.open(modelId.modelId));});
    }
}
