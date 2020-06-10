using UnityEngine;

public class UserAddAddressInput: IAPIModel {
    public int userId;

    public int addressId;

    public override IAPIModel fromJson(string json){
        return JsonUtility.FromJson<UserAddAddressInput>(json);
    }
}