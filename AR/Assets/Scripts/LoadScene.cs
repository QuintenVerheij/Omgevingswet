using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void SceneLoader(int SceneIndex)
    {
        if(SceneManager.GetActiveScene().buildIndex != SceneIndex) {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}
