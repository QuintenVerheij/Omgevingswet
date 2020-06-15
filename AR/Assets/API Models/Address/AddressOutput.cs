using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

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

    public static AddressOutput fromJson(string json){
        return JsonConvert.DeserializeObject<AddressOutput>(json);
    }

    public override string ToString() {
        return String.Format("id: {0}, city: {1}, street: {2}, houseNumber: {3}, houseNumberAddition: {4}, postalCode: {5}, Users: {6}",
        this.id,
        this.city,
        this.street,
        this.houseNumber,
        this.houseNumberAddition,
        this.postalCode,
        this.users);
    }
}