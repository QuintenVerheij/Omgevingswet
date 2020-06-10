using UnityEngine;

public class AuthorizationTokenReturn : IAPIModel {
    public bool successful;
    public MessageType messageType;
    public string message;
    public int userId;
    public AuthRoleSpecification role;
    public long expireTime;
    public string token;
    public static AuthorizationTokenReturn fromJson(string json){
        return JsonUtility.FromJson<AuthorizationTokenReturn>(json);
    }

    public override string ToString(){
        return string.Format("successful: {0}, messageType: {1}, message: {2}, userId: {3}, role: {4}, expireTime: {5}, token: {6}",
        this.successful,
        this.messageType.ToString("g"),
        this.message,
        this.userId,
        this.role,
        this.expireTime,
        this.token);
    }
}