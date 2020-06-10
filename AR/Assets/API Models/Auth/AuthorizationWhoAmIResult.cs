using UnityEngine;

public class AuthorizationWhoAmIResult: IAPIModel {
    public int userId;
    public AuthRoleSpecification role;
    public long expireTime;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationWhoAmIResult>(json);
    }
}