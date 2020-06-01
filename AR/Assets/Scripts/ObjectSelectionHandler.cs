using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectionHandler : BaseModeInputHandler {
    public static ObjectSelectionHandler Instance { get; private set; }
    private HashSet<Model> selectedModels = new HashSet<Model>();
    //PreTransformationData preTransformData;

    /*public class PreTransformationData {
        public Vector3 Eulers { get; private set; }
        public Vector3 Scale { get; private set; }

        public PreTransformationData(Vector3 eulers, Vector3 scale) {
            this.Eulers = eulers;
            this.Scale = scale;
        }
    }*/

    void Awake()
    {
        Instance = this;
        //preTransformData = new PreTransformationData(transform.eulerAngles, transform.localScale);
    }

    /*private void CheckModelOnScreenPoint(Touch touch) {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            Model model = hit.transform.GetComponent<Model>();
            if (model) {
                OnModelPress(model);
            }
        }
    }

    private void OnModelPress(Model model) {
        
    }*/

    private void MoveModels(Touch touch) {
        Vector2 delta = touch.deltaPosition;
        delta.x /= Screen.width;
        delta.y /= Screen.height;
        foreach (var model in selectedModels) {
            model.transform.position += new Vector3(delta.x,0,delta.y);
        }
    }

    public override void OnScreenPointHitEnd(RaycastHit hit, Vector3 startPosition, Vector3 currentPosition) {
        Model model = hit.transform.GetComponent<Model>();
        if (model) {
            if ((startPosition - currentPosition).magnitude <= float.Epsilon) {
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
    }

    public override void OnPlaneTouchMove(Vector3 delta) {
        foreach(var model in selectedModels) {
            model.transform.position += delta;
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
            model.transform.localScale *= 1 + scaleDelta;
        }
    }

    // Update is called once per frame
    /*void Update(){
        int touchCount = Input.touchCount;
        if(selectedModels.Count > 0 && touchCount > 0) {
            if(touchCount == 1) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    CheckModelOnScreenPoint(touch);
                }
                else if(touch.phase == TouchPhase.Moved){
                    MoveModels(touch);
                }
            }
        }
    }*/
}
