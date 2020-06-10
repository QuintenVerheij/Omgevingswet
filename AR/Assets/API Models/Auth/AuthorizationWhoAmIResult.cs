using UnityEngine;

public class AuthorizationWhoAmIResult: IAPIModel {
    public int userId;
    public AuthRoleSpecification role;
    public long expireTime;

    public AuthorizationWhoAmIResult(int userId, AuthRoleSpecification role, long expireTime)
    {
        this.userId = userId;
        this.role = role;
        this.expireTime = expireTime;
    }

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationWhoAmIResult>(json);
    }
}