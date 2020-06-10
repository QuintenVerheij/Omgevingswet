using UnityEngine;

public class AuthorizationTokenReturn : IAPIModel {
    public bool successful;
    public MessageType messageType;
    public string message;
    public int userId;
    public AuthRoleSpecification role;
    public long expireTime;
    public string token;
    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationTokenReturn>(json);
    }
}