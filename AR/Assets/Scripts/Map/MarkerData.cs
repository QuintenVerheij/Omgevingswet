using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MarkerData : MonoBehaviour
{
    [JsonProperty("id")]
    public int id { get; set; }
    [JsonProperty("userId")]
    public int userId { get; set; }
    [JsonProperty("public")]
    public bool publicBool { get; set; }
    [JsonProperty("visibleRange")]
    public int visibleRange { get; set; }
    [JsonProperty("longitude")]
    public float longitude { get; set; }
    [JsonProperty("latitude")]
    public float latitude { get; set; }
    [JsonProperty("createdAt")]
    public string createdAt { get; set; }
    [JsonProperty("preview")]
    public string preview { get; set; }
}