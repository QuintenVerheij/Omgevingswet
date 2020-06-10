using System.Collections.Generic;
using UnityEngine;

public class UserOutput : IAPIModel {
    public int id;
    public string username;
    public string email;
    public List<AddressCreateInput> address;
    public UserOutput(int id, string username, string email, List<AddressCreateInput> address)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.address = address;
    }

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutput>(json);
    }
}