using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class currentUser
{
    public static string path;
    protected class User
    {
        public int userId { get; set; }
        public string token { get; set; }

        public static User fromJson(string Json)
        {
            return JsonConvert.DeserializeObject<User>(Json);
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    // private string path = "Assets/Resources/currentUser.txt";
    public string readToken()
    {
        StreamReader reader = new StreamReader(path);
        User currentUser = User.fromJson(reader.ReadToEnd());
        reader.Close();
        return currentUser.token;
    }

    public int readUserId()
    {
        StreamReader reader = new StreamReader(path);
        User currentUser = User.fromJson(reader.ReadToEnd());
        reader.Close();
        return currentUser.userId;
    }

    public void writeToken(string token)
    {
        StreamReader reader = new StreamReader(path);
        User currentUser = User.fromJson(reader.ReadToEnd());
        reader.Close();

        currentUser.token = token;

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(currentUser.toJson());
        writer.Close();
    }

    public void writeUserId(int userId)
    {
        StreamReader reader = new StreamReader(path);
        User currentUser = User.fromJson(reader.ReadToEnd());
        reader.Close();

        currentUser.userId = userId;

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(currentUser.toJson());
        writer.Close();
    }
}
