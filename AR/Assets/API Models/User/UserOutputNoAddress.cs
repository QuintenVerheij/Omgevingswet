using UnityEngine;

public class UserOutputNoAddress : IAPIModel {
    public string username;
    public string email;

    public UserOutputNoAddress(string username, string email)
    {
        this.username = username;
        this.email = email;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutputNoAddress>(json);
    }
}