using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenARBuiltIn : MonoBehaviour
{
    public ARSceneOpener opener;
    public int builtInModelIndex;

    public void OpenScene() {
        opener.OpenARScene(builtInModelIndex);
    }
}
