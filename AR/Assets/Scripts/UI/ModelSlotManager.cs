using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSlotManager : MonoBehaviour{
    public Camera thumbnailCamera;
    public ModelSlot[] slots;

    public int currentPage = 0;
    public static ModelSlotManager Instance { get; private set; }

    public GameObject navigationUIGroup;

    private void Awake() {
        Instance = this;
    }

    private void OnEnable() {
        UpdateSlots();
    }

    public void UpdateSlots() {
        int slotCount = slots.Length;
        int prefabCount = ObjectCreationHandler.Instance.models.Count;

        navigationUIGroup.SetActive(prefabCount > slotCount);
        if (prefabCount <= slotCount) {
            currentPage = 0;
        }

        for (int i = 0; i < slotCount; i++) {
            if (i + slotCount * currentPage < prefabCount) {
                slots[i].gameObject.SetActive(true);
                slots[i].AttachModel(i + slotCount * currentPage, thumbnailCamera);
                //slots[i].AttachModel(i + slotCount * currentPage, thumbnailCamera, 2, Quaternion.identity);
            }
            else {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    public void NextPage() {
        int slotCount = slots.Length;
        int prefabCount = ObjectCreationHandler.Instance.models.Count;
        int lastPageIndex = prefabCount / slotCount;

        currentPage += 1;
        if (currentPage > lastPageIndex) {
            currentPage = 0;
        }
        UpdateSlots();
    }

    public void PreviousPage() {
        int slotCount = slots.Length;
        int prefabCount = ObjectCreationHandler.Instance.models.Count;
        int lastPageIndex = prefabCount / slotCount;

        currentPage -= 1;
        if(currentPage < 0) {
            currentPage = lastPageIndex;
        }
        UpdateSlots();
    }
}
