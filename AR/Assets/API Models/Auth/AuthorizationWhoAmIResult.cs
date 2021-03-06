using System;
using UnityEngine;
using Newtonsoft.Json;

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

    public static AuthorizationWhoAmIResult fromJson(string json){
        return JsonConvert.DeserializeObject<AuthorizationWhoAmIResult>(json);
    }

    public override string ToString() {
        return String.Format("userId: {0}, role: {1}, expireTime: {2}",
        this.userId,
        this.role,
        this.expireTime);
    }
}