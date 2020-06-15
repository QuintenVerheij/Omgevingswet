using System;
using UnityEngine;
using Newtonsoft.Json;

public class UserAddAddressInput: IAPIModel {
    public int userId;

    public int addressId;

    public UserAddAddressInput(int userId, int addressId)
    {
        this.userId = userId;
        this.addressId = addressId;
    }

    public static UserAddAddressInput fromJson(string json){
        return JsonConvert.DeserializeObject<UserAddAddressInput>(json);
    }
    public override string ToString() {
        return String.Format("userId: {0}, addressId: {1}",
        this.userId,
        this.addressId);
    }
}