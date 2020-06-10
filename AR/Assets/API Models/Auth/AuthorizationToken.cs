using UnityEngine;

public class AuthorizationToken : IAPIModel {
    public string token;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationToken>(json);
    }
}