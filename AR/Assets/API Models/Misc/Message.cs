using System;
using UnityEngine;
public class Message : IAPIModel {
    public bool successful;

    public MessageType messageType;

    public AuthorizationType authorizationType;

    public string message;

    public int targetId;

    public int userId;

    public Message(bool successful, MessageType messageType, AuthorizationType authorizationType, string message, int targetId, int userId)
    {
        this.successful = successful;
        this.messageType = messageType;
        this.authorizationType = authorizationType;
        this.message = message;
        this.targetId = targetId;
        this.userId = userId;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<Message>(json);
    }

    public override string ToString() {
        return String.Format("successful: {0}, messageType: {1}, authorizationType: {2}, message: {3}, targetId: {4}, userId: {5}",
        this.successful,
        this.messageType.ToString("g"),
        this.authorizationType.ToString("g"),
        this.message,
        this.targetId,
        this.userId);
    }
}