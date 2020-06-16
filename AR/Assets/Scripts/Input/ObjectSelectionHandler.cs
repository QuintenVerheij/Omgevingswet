using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectionHandler : BaseModeInputHandler {
    public enum PlacementMode {
        XZ, Y
    }

    public static ObjectSelectionHandler Instance { get; private set; }
    private HashSet<Model> selectedModels = new HashSet<Model>();
    public GameObject uiGroup_selections;
    public Transform placedModelFolder;

    public PlacementMode ObjectMovementMode { get; private set; }
    public GameObject gridPlane_xz;
    public GridDisplay gridDisplay;
    public float yAxisMovementSensitivity = 10;

    //public LayerMask exportInclusionMask;

    void Awake()
    {
        Instance = this;
    }

    private void OnEnable() {
        uiGroup_selections.SetActive(true);
    }

    private void OnDisable() {
        if(uiGroup_selections)
            uiGroup_selections.SetActive(false);
        foreach (var model in selectedModels) {
            model.SetHighlight(false);
        }
        selectedModels.Clear();
        ObjectMovementMode = PlacementMode.XZ;
    }

    public override void OnScreenPointHitEnd(RaycastHit hit, Vector3 startPosition, Vector3 currentPosition) {
        Model model = hit.transform.GetComponent<Model>();
        if (!model) {
            var modelPart = hit.transform.GetComponent<CombinedModelPart>();
            if(modelPart)
                model = modelPart.model;
        }
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
        if(ObjectMovementMode == PlacementMode.XZ) {
            foreach (var model in selectedModels) {
                model.transform.position += new Vector3(delta.x, 0, delta.z);
            }
        }

        /*foreach (var model in selectedModels) {
            model.transform.position += delta;
        }*/
    }

    public override void OnScreenPointMove(Vector3 delta) {
        delta /= Screen.height;
        if (ObjectMovementMode == PlacementMode.Y) {
            foreach (var model in selectedModels) {
                model.transform.position += new Vector3(0, delta.y * yAxisMovementSensitivity, 0);
            }
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

    public void NextMovementMode() {
        ObjectMovementMode = (ObjectMovementMode == PlacementMode.XZ) ? PlacementMode.Y : PlacementMode.XZ;
    }

    public bool CanCombineSelectedModels() {
        Model[] modelArray = new Model[selectedModels.Count];
        selectedModels.CopyTo(modelArray);
        return JSONModelUtility.CanCombineModels(modelArray);
    }
    public void CombineSelectedModels() {
        Model[] modelArray = new Model[selectedModels.Count];
        selectedModels.CopyTo(modelArray);
        if (CanCombineSelectedModels()) {
            CombinedModel obj = JSONModelUtility.CombineModels(modelArray, placedModelFolder, $"Custom Model-{System.DateTime.Now.Ticks}");
            ObjectCreationHandler.Instance.AddCustomModel(obj);
            //JSONCombinedModel jsonCombinedModel = new JSONCombinedModel(obj);
            JSONModelUtility.ExportCustomModel(obj.transform.name, obj);

            foreach (var model in selectedModels) {
                model.SetHighlight(false);
            }
            selectedModels.Clear();
        }
    }
}
