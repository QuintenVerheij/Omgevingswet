using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAPIModel
{
    public byte[] toJsonRaw() {
        string json = JsonUtility.ToJson(this);
        byte[] jsonRaw = new System.Text.UTF8Encoding().GetBytes(json);

        return jsonRaw;
    }


}
