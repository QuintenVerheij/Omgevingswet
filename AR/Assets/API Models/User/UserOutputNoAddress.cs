using System;
using UnityEngine;
using Newtonsoft.Json;

public class UserOutputNoAddress : IAPIModel {
    public string username;
    public string email;

    public UserOutputNoAddress(string username, string email)
    {
        this.username = username;
        this.email = email;
    }

    public static UserOutputNoAddress fromJson(string json){
        return JsonConvert.DeserializeObject<UserOutputNoAddress>(json);
    }

    public override string ToString() {
        return String.Format("username: {0}, email: {1}",
        this.username,
        this.email);
    }
}