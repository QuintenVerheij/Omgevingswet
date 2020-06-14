using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class UserOutput : IAPIModel
{
    public int id;
    public string username;
    public string email;
    public List<AddressCreateInput> address;

    public List<ModelOutputPreview> models;
    public UserOutput(int id, string username, string email, List<AddressCreateInput> address, List<ModelOutputPreview> models)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.address = address;
        this.models = models;
    }

    public static UserOutput fromJson(string json)
    {
        return JsonConvert.DeserializeObject<UserOutput>(json);
    }

    public override string ToString()
    {
        return String.Format("id: {0}, username: {1}, email: {2}, address: {3}, models: {4}",
        this.id,
        this.username,
        this.email,
        string.Join(",", this.address.Select<AddressCreateInput, string>(x => x.ToString())),
        string.Join(",", this.models.Select<ModelOutputPreview, string>(x => x.ToString())));
    }
}