using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneWithUserId : MonoBehaviour
{
    public void SceneLoader(int userId)
    {
        idToCross.crossingId = userId;
        SceneManager.LoadScene(3);
    }
}

public static class idToCross {
    public static int crossingId {get; set;}
}