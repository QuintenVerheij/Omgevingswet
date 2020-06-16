using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabNameText : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARInputState.Instance.CurrentMode == ARInputState.Mode.AddObjects) {
            ObjectCreationHandler handler = ObjectCreationHandler.Instance;
            int index = handler.currentIndex;
            int modelCount = handler.models.Count;
            GameObject currentPrefab = handler.models[index].gameObject;
            text.text = currentPrefab.name + " (" + (index+1) +"/"+ modelCount + ")";
        }
    }
}
