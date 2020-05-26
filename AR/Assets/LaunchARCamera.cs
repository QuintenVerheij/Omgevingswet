using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManager;

public class LaunchARCamera : MonoBehaviour
{
    void OnMouseUp() {
        SceneManager.LoadScene("ModelDraaien", LoadSceneMode.single);
    }
}
