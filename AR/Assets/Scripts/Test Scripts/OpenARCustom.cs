using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenARCustom : MonoBehaviour
{
    public ARSceneOpener opener;
    [TextArea]
    public string customModelJSON;

    public void OpenScene() {
        opener.OpenARScene(customModelJSON);
    }
}
