using System;
using UnityEngine;
using Newtonsoft.Json;

public class AuthorizationTokenRequest : IAPIModel {
    public string mail;

    public string password;

    public AuthorizationTokenRequest(string mail, string password)
    {
        this.mail = mail;
        this.password = password;
    }

    public static AuthorizationTokenRequest fromJson(string json){
        return JsonConvert.DeserializeObject<AuthorizationTokenRequest>(json);
    }

    public override string ToString() {
        return String.Format("mail: {0}, password: {1}",
        this.mail,
        this.password);
    }
}