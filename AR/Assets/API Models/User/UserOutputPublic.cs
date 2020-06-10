using UnityEngine;

public class UserOutputPublic: IAPIModel {
    public int id;
    public string username;

    public UserOutputPublic(int id, string username)
    {
        this.id = id;
        this.username = username;
    }

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutputPublic>(json);
    }
}