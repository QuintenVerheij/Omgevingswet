using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class LoadSceneWithUserId
{
    public static void SceneLoader(int userId)
    {
        ReadProfile.crossedId = userId;
        SceneManager.LoadScene(3);
    }
}