using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneWithModelPath : MonoBehaviour
{
    public void SceneLoader(string modelPath, string previewPath)
    {
        SaveModel.modelPath = modelPath;
        SaveModel.previewPath = previewPath;
        SceneManager.LoadScene(5);
    }
}