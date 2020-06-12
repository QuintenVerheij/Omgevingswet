using System;
using UnityEngine;
using Newtonsoft.Json;

public class MessageWithItem<T> : IAPIModel {
    public Message message;
    public T item;

    public MessageWithItem(Message message, T item)
    {
        this.message = message;
        this.item = item;
    }

    public static MessageWithItem<T> fromJson(string json){
        return JsonConvert.DeserializeObject<MessageWithItem<T>>(json);
    }

    public override string ToString() {
        return String.Format("message: {0}, item: {1}",
        this.message.ToString(),
        this.item.ToString());
    }
}