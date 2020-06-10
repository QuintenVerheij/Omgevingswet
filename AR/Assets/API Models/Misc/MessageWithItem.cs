using UnityEngine;

public class MessageWithItem<T> : IAPIModel {
    public Message message;
    public T item;
    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<MessageWithItem<T>>(json);
    }
}