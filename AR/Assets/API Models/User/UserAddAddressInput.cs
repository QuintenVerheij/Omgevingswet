using UnityEngine;

public class UserAddAddressInput: IAPIModel {
    public int userId;

    public int addressId;

    public UserAddAddressInput(int userId, int addressId)
    {
        this.userId = userId;
        this.addressId = addressId;
    }

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserAddAddressInput>(json);
    }
}