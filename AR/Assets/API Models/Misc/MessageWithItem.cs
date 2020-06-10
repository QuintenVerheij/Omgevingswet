using UnityEngine;

public class MessageWithItem<T> : IAPIModel {
    public Message message;
    public T item;

    public MessageWithItem(Message message, T item)
    {
        this.message = message;
        this.item = item;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<MessageWithItem<T>>(json);
    }
}