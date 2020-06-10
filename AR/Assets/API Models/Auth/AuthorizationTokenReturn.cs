using UnityEngine;

public class AuthorizationTokenReturn : IAPIModel {
    public bool successful;
    public MessageType messageType;
    public string message;
    public int userId;
    public AuthRoleSpecification role;
    public long expireTime;
    public string token;

    public AuthorizationTokenReturn(bool successful, MessageType messageType, string message, int userId, AuthRoleSpecification role, long expireTime, string token)
    {
        this.successful = successful;
        this.messageType = messageType;
        this.message = message;
        this.userId = userId;
        this.role = role;
        this.expireTime = expireTime;
        this.token = token;
    }

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AuthorizationTokenReturn>(json);
    }
}