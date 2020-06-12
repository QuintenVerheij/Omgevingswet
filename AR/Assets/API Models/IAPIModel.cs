using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public abstract class IAPIModel
{
    public byte[] toJsonRaw() {
        string json = JsonConvert.SerializeObject(this);
        byte[] jsonRaw = new System.Text.UTF8Encoding().GetBytes(json);

        return jsonRaw;
    }


}
