using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ModelPageNumber : MonoBehaviour
{
    public TMP_Text text;
    
    void Update()
    {
        int slotCount = ModelSlotManager.Instance.slots.Length;
        int prefabCount = ObjectCreationHandler.Instance.models.Length;
        int lastPageIndex = prefabCount / slotCount + 1;

        int page = ModelSlotManager.Instance.currentPage + 1;
        text.text = page.ToString() + "/" + lastPageIndex.ToString();
    }
}
