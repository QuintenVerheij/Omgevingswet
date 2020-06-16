using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickViewProfile : MonoBehaviour
{
    public Button btn;
    private int _userID;
    public void SetUserID(int userID)
    {
        this._userID = userID;
    }
    void Start()
    {
        btn.onClick.AddListener(delegate { new LoadSceneWithUserId().SceneLoader(_userID); });
    }
}
