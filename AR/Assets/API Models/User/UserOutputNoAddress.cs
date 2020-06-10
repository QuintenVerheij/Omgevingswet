using UnityEngine;

public class UserOutputNoAddress : IAPIModel {
    public string username;
    public string email;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutputNoAddress>(json);
    }
}