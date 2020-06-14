using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class UserOutputPublic : IAPIModel
{
    public int id;
    public string username;

    public List<ModelOutputPreview> models;

    public UserOutputPublic(int id, string username, List<ModelOutputPreview> models)
    {
        this.id = id;
        this.username = username;
        this.models = models;
    }

    public static UserOutputPublic fromJson(string json)
    {
        return JsonConvert.DeserializeObject<UserOutputPublic>(json);
    }

    public override string ToString()
    {
        return String.Format("id: {0}, username: {1}, models: {2}",
        this.id,
        this.username,
        string.Join(",", this.models.Select<ModelOutputPreview, string>(x => x.ToString())));
    }
}