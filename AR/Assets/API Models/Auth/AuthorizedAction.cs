using UnityEngine;

public class AuthorizedAction<T>:IAPIModel {
    public AuthorizationToken auth;
    public T input;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizedAction<T>>(json);
    }
}