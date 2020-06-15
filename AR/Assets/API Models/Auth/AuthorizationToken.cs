using UnityEngine;
using Newtonsoft.Json;

public class AuthorizationToken : IAPIModel {
    public string token;

    public AuthorizationToken(string token)
    {
        this.token = token;
    }

    public static AuthorizationToken fromJson(string json){
        return JsonConvert.DeserializeObject<AuthorizationToken>(json);
    }

    public override string ToString() {
        return this.token;
    }
}