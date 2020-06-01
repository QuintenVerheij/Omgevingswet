using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : BaseModeInputHandler {
    public PlacementIndicator placementIndicator;
    public GameObject environment; //environment objects & visible plane only, used for scaling and rotating
    public GameObject environmentScene; //environment + plane collider, used for positioning

    public GameObject uiGroup_environment;

    private Vector3 beginScale;

    public static EnvironmentHandler Instance { get; private set; }
    private void Awake() {
        Instance = this;
        beginScale = transform.localScale;
    }

    void Start() {
        environment.SetActive(false);
    }

    private void OnEnable() {
        placementIndicator.gameObject.SetActive(true);
        uiGroup_environment.SetActive(true);
    }

    private void OnDisable() {
        placementIndicator.gameObject.SetActive(false);
        uiGroup_environment.SetActive(false);
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

    public override void OnPlaneTouchBegin(Vector3 position) {
        
    }

    public void Place() {
        environment.SetActive(true);
        environmentScene.transform.position = placementIndicator.transform.position;
        environmentScene.transform.rotation = placementIndicator.transform.rotation;
    }
}
