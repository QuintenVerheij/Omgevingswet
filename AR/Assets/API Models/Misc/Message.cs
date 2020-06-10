using UnityEngine;
public class Message : IAPIModel {
    public bool successful;

    public MessageType messageType;

    public AuthorizationType authorizationType;

    public string message;

    public int targetId;

    public int userId;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<Message>(json);
    }
}