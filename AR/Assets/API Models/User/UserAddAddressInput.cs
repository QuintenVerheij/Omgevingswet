using System;
using UnityEngine;

public class UserAddAddressInput: IAPIModel {
    public int userId;

    public int addressId;

    public UserAddAddressInput(int userId, int addressId)
    {
        this.userId = userId;
        this.addressId = addressId;
    }

    public static IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserAddAddressInput>(json);
    }
    public override string ToString() {
        return String.Format("userId: {0}, addressId: {1}",
        this.userId,
        this.addressId);
    }
}