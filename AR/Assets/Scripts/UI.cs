using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public InputField userName;
    public InputField password;
    public DataManager dataManager;

    void Start()
    {
        dataManager.Load();
        userName.text = dataManager.data.username;
        password.text = dataManager.data.password;
    }
    
    public void InsertUsername(string text)
    {
        dataManager.data.username = text;
    }

    public void InsertPassword(string text)
    {
        dataManager.data.password = text;
    }

    public void ClickSave()
    {
        dataManager.Save();
    }
}
