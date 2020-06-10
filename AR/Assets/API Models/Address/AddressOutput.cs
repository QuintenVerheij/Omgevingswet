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

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AddressOutput>(json);
    }
}