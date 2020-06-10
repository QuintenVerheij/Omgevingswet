using UnityEngine;

public class AuthorizationTokenRequest : IAPIModel {
    public string mail;

    public string password;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationTokenRequest>(json);
    }
}