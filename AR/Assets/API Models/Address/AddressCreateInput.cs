using System;
using UnityEngine;

public class AddressCreateInput : IAPIModel {
    public string city;
    public string street;
    public int houseNumber;
    public string houseNumberAddition;
    public string postalCode;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AddressCreateInput>(json);
    }
}