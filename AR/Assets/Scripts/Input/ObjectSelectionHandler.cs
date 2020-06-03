using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectionHandler : BaseModeInputHandler {
    public static ObjectSelectionHandler Instance { get; private set; }
    private HashSet<Model> selectedModels = new HashSet<Model>();
    public GameObject uiGroup_selections;

    void Awake()
    {
        Instance = this;
    }

    private void OnEnable() {
        uiGroup_selections.SetActive(true);
    }
    private void OnDisable() {
        uiGroup_selections.SetActive(false);
        foreach (var model in selectedModels) {
            model.SetHighlight(false);
        }
        selectedModels.Clear();
    }

    public override void OnScreenPointHitEnd(RaycastHit hit, Vector3 startPosition, Vector3 currentPosition) {
        Model model = hit.transform.GetComponent<Model>();
        if (model) {
            bool selected = selectedModels.Contains(model);
            model.SetHighlight(!selected);
            if (selected) {
                selectedModels.Remove(model);
            }
            else {
                selectedModels.Add(model);
            }
        }
    }

    public override void OnPlaneTouchMove(Vector3 delta) {
        foreach(var model in selectedModels) {
            model.transform.position += new Vector3(delta.x, 0, delta.z);
        }
    }

    public override void OnMultiTouchRotate(float angleDelta) {
        foreach (var model in selectedModels) {
            Vector3 eulers = model.transform.eulerAngles;
            eulers.y += angleDelta;
            model.transform.eulerAngles = eulers;
        }
    }

    public override void OnMultiTouchScale(float scaleDelta) {
        foreach (var model in selectedModels) {
            model.transform.localScale *= scaleDelta;
        }
    }

    public void RemoveSelectedObjects() {
        Model[] models = new Model[selectedModels.Count];
        selectedModels.CopyTo(models);
        selectedModels.Clear();
        for (int i = 0; i < models.Length; i++) {
            Destroy(models[i].gameObject);
        }
    }
}
