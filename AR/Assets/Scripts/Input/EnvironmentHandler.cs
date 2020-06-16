using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : BaseModeInputHandler {
    public PlacementIndicator placementIndicator;
    public GameObject environment; //environment objects & visible plane only, used for scaling and rotating
    public GameObject environmentScene; //environment + plane collider, used for positioning
    public GameObject collisionPlane_environment;

    public GameObject uiGroup_environment;

    public Camera arCamera;
    public string arLayer;

    public GridDisplay gridDisplay;

    public static EnvironmentHandler Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }

    void Start() {
        environment.SetActive(false);
    }

    private void SetARLayerVisibility(bool visible) {
        if (arCamera) {
            int layer = LayerMask.NameToLayer("AR");
            if (visible) {
                arCamera.cullingMask |= (1 << layer);
            }
            else {
                arCamera.cullingMask &= ~(1 << layer);
            }
        }
    }

    private void OnEnable() {
        if(placementIndicator)
            placementIndicator.gameObject.SetActive(true);
        if(uiGroup_environment)
            uiGroup_environment.SetActive(true);
        gridDisplay.SetGridDisplay(false);
        SetARLayerVisibility(true);
    }

    private void OnDisable() {
        if(placementIndicator)
            placementIndicator.gameObject.SetActive(false);
        if(uiGroup_environment)
            uiGroup_environment.SetActive(false);
        
        SetARLayerVisibility(false);
    }

    public override void OnPlaneTouchMove(Vector3 delta) {
        if (environment.activeInHierarchy) {
            environmentScene.transform.position += delta;
        }
    }

    public override void OnMultiTouchRotate(float angleDelta) {
        if (environment.activeInHierarchy) {
            Vector3 eulers = environment.transform.eulerAngles;
            eulers.y += angleDelta;
            environment.transform.eulerAngles = eulers;
        }
    }

    public override void OnMultiTouchScale(float scaleDelta) {
        if (environment.activeInHierarchy) {
            environment.transform.localScale *= scaleDelta;
        }
    }

    public void Place() {
        environment.SetActive(true);
        environmentScene.transform.position = placementIndicator.transform.position;
        environmentScene.transform.rotation = placementIndicator.transform.rotation;
    }
}
