using System;
using UnityEngine;
using Newtonsoft.Json;

public class UserOutputPublic: IAPIModel {
    public int id;
    public string username;

    public UserOutputPublic(int id, string username)
    {
        this.id = id;
        this.username = username;
    }

    public static UserOutputPublic fromJson(string json){
        return JsonConvert.DeserializeObject<UserOutputPublic>(json);
    }

    public override string ToString() {
        return String.Format("id: {0}, username: {1}",
        this.id,
        this.username);
    }
}