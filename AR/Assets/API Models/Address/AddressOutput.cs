using System.Collections.Generic;
using UnityEngine;

public class AddressOutput : IAPIModel {
    public int id;
    public string city;
    public string street;
    public int houseNumber;
    public string houseNumberAddition;
    public string postalCode;
    public List<UserOutputNoAddress> users;

    public AddressOutput(int id, string city, string street, int houseNumber, string houseNumberAddition, string postalCode, List<UserOutputNoAddress> users)
    {
        this.id = id;
        this.city = city;
        this.street = street;
        this.houseNumber = houseNumber;
        this.houseNumberAddition = houseNumberAddition;
        this.postalCode = postalCode;
        this.users = users;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AddressOutput>(json);
    }
}