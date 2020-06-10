using UnityEngine;

public class UserOutputPublic: IAPIModel {
    public int id;
    public string username;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutputPublic>(json);
    }
}