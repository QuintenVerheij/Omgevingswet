﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class CollectForm : MonoBehaviour
{
    Form formCollector;
    public InputField userInputField;
    public InputField passInputField;
    public InputField mailInputField;
    public InputField cityInputField;
    public InputField strtInputField;
    public InputField numbInputField;
    public InputField nextInputField;
    public InputField postInputField;
    public Button submitButton;
    public string url = AppStartup.APIURL + ":8080/user/create";
    void Start()
    {
        formCollector = new Form();
        userInputField.onEndEdit.AddListener(delegate { InputHandler(0, userInputField.text); });
        passInputField.onEndEdit.AddListener(delegate { InputHandler(1, passInputField.text); });
        mailInputField.onEndEdit.AddListener(delegate { InputHandler(2, mailInputField.text); });
        cityInputField.onEndEdit.AddListener(delegate { InputHandler(3, cityInputField.text); });
        strtInputField.onEndEdit.AddListener(delegate { InputHandler(4, strtInputField.text); });
        numbInputField.onEndEdit.AddListener(delegate { InputHandler(5, numbInputField.text); });
        nextInputField.onEndEdit.AddListener(delegate { InputHandler(6, nextInputField.text); });
        postInputField.onEndEdit.AddListener(delegate { InputHandler(7, postInputField.text); });
        submitButton.onClick.AddListener(delegate {submit();});
    }
    public void InputHandler(int which, string input)
    {
        if (which == 0)
        {
            formCollector.Username = input;
        }
        else if (which == 1)
        {
            formCollector.Password = input;
        }
        else if (which == 2)
        {
            formCollector.Email = input;
        }
        else if (which == 3)
        {
            formCollector.City = input;
        }
        else if (which == 4)
        {
            formCollector.Street = input;
        }
        else if (which == 5)
        {
            formCollector.Number = input;
        }
        else if (which == 6)
        {
            formCollector.NumExt = input;
        }
        else if (which == 7)
        {
            formCollector.PostCode = input;
        }
    }
    public void ClickSubmit()
    {
        StartCoroutine(submit());
    }
    public IEnumerator submit()
    {
        UserCreateInput res = new UserCreateInput(
            formCollector.Username,
            formCollector.Email,
            formCollector.Password,
            new AddressCreateInput(
                formCollector.City,
                formCollector.Street,
                int.Parse(formCollector.Number),
                formCollector.NumExt,
                formCollector.PostCode
            )
        );
        Debug.Log(res.ToString());
        byte[] json = res.toJsonRaw();
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(json);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Message message = (Message)Message.fromJson(www.downloadHandler.text);
            if (message.successful)
            {
                Debug.Log("Success");
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log(message.message);
            }
        }
    }
}
public class Form
{
    public string Username;
    public string Password;
    public string Email;
    public string City;
    public string Street;
    public string Number;
    public string NumExt;
    public string PostCode;
    public Form()
    {
        Username = "";
        Password = "";
        Email = "";
        City = "";
        Street = "";
        Number = "";
        NumExt = "";
        PostCode = "";
    }
}

