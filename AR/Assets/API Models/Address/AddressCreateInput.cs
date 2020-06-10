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

    public override string ToString() {
        return String.Format("city: {0}, street: {1}, houseNumber: {2}, houseNumberAddition: {3}, postalCode: {4}",
        this.city,
        this.street,
        this.houseNumber,
        this.houseNumberAddition,
        this.postalCode);
    }
}