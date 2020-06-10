using UnityEngine;

public class AuthorizationToken : IAPIModel {
    public string token;

    public AuthorizationToken(string token)
    {
        this.token = token;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationToken>(json);
    }
}