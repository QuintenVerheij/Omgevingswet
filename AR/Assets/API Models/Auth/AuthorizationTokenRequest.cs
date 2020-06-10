using UnityEngine;

public class AuthorizationTokenRequest : IAPIModel {
    public string mail;

    public string password;

    public AuthorizationTokenRequest(string mail, string password)
    {
        this.mail = mail;
        this.password = password;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationTokenRequest>(json);
    }
}