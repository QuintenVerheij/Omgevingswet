using System;
using UnityEngine;

public class AuthorizedAction<T>:IAPIModel {
    public AuthorizationToken auth;
    public T input;

    public AuthorizedAction(AuthorizationToken auth, T input)
    {
        this.auth = auth;
        this.input = input;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizedAction<T>>(json);
    }

    public override string ToString() {
        return String.Format("auth: {0}, input: {1}",
        auth.ToString(),
        input.ToString());
    }
}