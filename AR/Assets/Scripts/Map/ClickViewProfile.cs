using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickViewProfile : MonoBehaviour
{
    private int _userID;
    public Button button;
    public void SetUserID(int userID)
    {
        this._userID = userID;
    }

    void start()
    {
        LoadSceneWithUserId sl = new LoadSceneWithUserId();
        button.onClick.AddListener(() => sl.SceneLoader(_userID));
    }
}
