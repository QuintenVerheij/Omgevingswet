using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManager;
using UnityEngine;


public class OpenARCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void onMouseUp() {
        SceneManager.LoadScene("ModelDraaien", LoadSceneModule.single);
    }
}
