using UnityEngine;
public class UserCreateInput : IAPIModel {
    public string username;
    public string email;
    public string password;

    public AddressCreateInput address;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserCreateInput>(json);
    }
}