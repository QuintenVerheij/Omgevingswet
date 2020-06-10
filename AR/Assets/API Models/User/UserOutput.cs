using System.Collections.Generic;
using UnityEngine;

public class UserOutput : IAPIModel {
    public int id;
    public string username;
    public string email;
    public List<AddressCreateInput> address;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutput>(json);
    }
}