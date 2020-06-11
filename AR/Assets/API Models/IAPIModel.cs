using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.json.JsonConvert;

public abstract class IAPIModel
{
    public byte[] toJsonRaw() {
        string json = JsonConvert.SerializeObject(this);
        byte[] jsonRaw = new System.Text.UTF8Encoding().GetBytes(json);
        Debug.Log(json);
        return jsonRaw;
    }


}
