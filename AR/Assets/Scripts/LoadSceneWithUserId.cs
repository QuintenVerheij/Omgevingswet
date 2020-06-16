using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class LoadSceneWithUserId
{
    private int _userId;
    public void setUserId(int userId)
    {
        _userId = userId;
    }
    public void SceneLoader(int userId)
    {
        ReadProfile.crossedId = userId;
        SceneManager.LoadScene(3);
    }
    public void SceneLoader()
    {
        ReadProfile.crossedId = _userId;
        SceneManager.LoadScene(3);
    }
}