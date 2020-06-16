using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneWithUserId : MonoBehaviour
{
    public void SceneLoader(int userId)
    {
        ReadProfile.crossedId = userId;
        SceneManager.LoadScene(3);
    }
}