using System;
using UnityEngine;

public class AddressCreateInput : IAPIModel {

    public string city {get; set;}
    public string street {get; set;}
    public int houseNumber {get; set;}
    public string houseNumberAddition {get; set;}
    public string postalCode {get; set;}

    public AddressCreateInput(string city, string street, int houseNumber, string houseNumberAddition, string postalCode)
    {
        this.city = city;
        this.street = street;
        this.houseNumber = houseNumber;
        this.houseNumberAddition = houseNumberAddition;
        this.postalCode = postalCode;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<AddressCreateInput>(json);
    }
}