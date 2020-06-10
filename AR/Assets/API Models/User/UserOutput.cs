using System;
using System.Collections.Generic;
using System.Linq;
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

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserOutput>(json);
    }

    public override string ToString() {
        return String.Format("id: {0}, username: {1}, email: {2}, address: {3}",
        this.id,
        this.username,
        this.email,
        string.Join(",", this.address.Select<AddressCreateInput, string>(x => x.ToString())));
    }
}