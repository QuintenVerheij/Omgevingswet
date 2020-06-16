using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    private currentUser _cu;
    void Start()
    {
        _cu = new currentUser();
    }
    public void SceneLoader(int SceneIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneIndex)
        {
            if (SceneIndex == 3)
            {
                new LoadSceneWithUserId().SceneLoader(_cu.readUserId());
            }
            else
            {
                SceneManager.LoadScene(SceneIndex);
            }
        }
    }
}
