using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneOpener : MonoBehaviour
{
    public int arSceneIndex;
    private int builtinModelIndex = -1;
    private JSONCombinedModel customModel;

    //with built in model
    public void OpenARScene(int builtinModelIndex) {
        print("Opening AR scene with built-in model.");
        this.builtinModelIndex = builtinModelIndex;

        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(arSceneIndex);
    }

    //with custom model
    public void OpenARScene(string customModelJson) {
        print("Opening AR scene with custom model.");
        customModel = JSONCombinedModel.FromJSON(customModelJson);

        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(arSceneIndex);
    }

    public int GetBuiltInModelIndex() {
        return builtinModelIndex;
    }

    public JSONCombinedModel GetCustomModel() {
        return customModel;
    }
}
