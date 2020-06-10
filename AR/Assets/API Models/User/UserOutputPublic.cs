using System;
using UnityEngine;

public class UserOutputPublic: IAPIModel {
    public int id;
    public string username;

    public UserOutputPublic(int id, string username)
    {
        this.id = id;
        this.username = username;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutputPublic>(json);
    }

    public override string ToString() {
        return String.Format("id: {0}, username: {1}",
        this.id,
        this.username);
    }
}