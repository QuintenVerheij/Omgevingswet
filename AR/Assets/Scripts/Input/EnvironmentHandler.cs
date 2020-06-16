using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : BaseModeInputHandler {
    public PlacementIndicator placementIndicator;
    public GameObject environment; //environment objects & visible plane only, used for scaling and rotating
    public GameObject environmentScene; //environment + plane collider, used for positioning
    public GameObject collisionPlane_environment;

    public AR_ArgumentLoader argumentLoader;
    private bool argumentModelHasBeenPlaced = false;

    public GameObject uiGroup_environment;

    public Camera arCamera;
    public string arLayer;

    public GridDisplay gridDisplay;

    private Vector3 initialPosition;

    public static EnvironmentHandler Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }

    void Start() {
        environment.SetActive(false);
        initialPosition = environmentScene.transform.position;
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

    public override void OnPlaneTouchMove(Vector3 startPosition, Vector3 prevPosition, Vector3 currentPosition) {
        if (environment.activeInHierarchy) {
            Vector3 pos = this.initialPosition + (currentPosition - startPosition);
            environmentScene.transform.position = new Vector3(pos.x, environmentScene.transform.position.y, pos.z);
        }
    }

    public override void OnPlaneTouchBegin(Vector3 position) {
        this.initialPosition = environmentScene.transform.position;
    }
    public override void OnPlaneTouchEnd(Vector3 startPosition, Vector3 currentPosition) {
        this.initialPosition = environmentScene.transform.position;
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

        if (!argumentModelHasBeenPlaced) {
            argumentModelHasBeenPlaced = true;
            argumentLoader.PlaceArgumentModel();
        }
    }
}
