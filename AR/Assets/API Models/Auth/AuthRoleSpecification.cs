using System.Collections.Generic;
using UnityEngine;

public class AuthRoleSpecification: IAPIModel{
    public AuthorizationRole role;

    public List<string> allowedInfo;
    public List<string> allowedCreate;
    public List<string> allowedRead;
    public List<string> allowedUpdate;
    public List<string> allowedDelete;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthRoleSpecification>(json);
    }
}