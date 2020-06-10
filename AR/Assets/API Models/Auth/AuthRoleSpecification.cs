using System;
using System.Collections.Generic;
using UnityEngine;

public class AuthRoleSpecification: IAPIModel{
    public AuthorizationRole role;

    public List<string> allowedInfo;
    public List<string> allowedCreate;
    public List<string> allowedRead;
    public List<string> allowedUpdate;
    public List<string> allowedDelete;

    public AuthRoleSpecification(AuthorizationRole role, List<string> allowedInfo, List<string> allowedCreate, List<string> allowedRead, List<string> allowedUpdate, List<string> allowedDelete)
    {
        this.role = role;
        this.allowedInfo = allowedInfo;
        this.allowedCreate = allowedCreate;
        this.allowedRead = allowedRead;
        this.allowedUpdate = allowedUpdate;
        this.allowedDelete = allowedDelete;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthRoleSpecification>(json);
    }

    public override string ToString() {
        return String.Format("role: {0}, allowedInfo: {1},allowedCreate: {2},allowedRead: {3},allowedUpdate: {4}, allowedDelete: {5}",
        role.ToString("g"),
        string.Join(",", this.allowedInfo),
        string.Join(",", this.allowedCreate),
        string.Join(",", this.allowedRead),
        string.Join(",", this.allowedUpdate),
        string.Join(",", this.allowedDelete));
    }

}