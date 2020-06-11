using System;
using UnityEngine;
public class UserCreateInput : IAPIModel {
    public string username;
    public string email;
    public string password;

    public AddressCreateInput address;

    public UserCreateInput(string username, string email, string password, AddressCreateInput address)
    {
        this.username = username;
        this.email = email;
        this.password = password;
        this.address = address;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserCreateInput>(json);
    }

    public override string ToString() {
        return String.Format("username: {0}, email: {1}, password: {2}, address: {3}",
        this.username,
        this.email,
        this.password,
        this.address.ToString());
    }
}