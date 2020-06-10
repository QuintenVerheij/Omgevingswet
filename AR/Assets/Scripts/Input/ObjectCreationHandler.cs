using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreationHandler : BaseModeInputHandler {
    public int currentIndex;

    public GameObject[] models; //array of instances of models in the scene, modify this array in the inspector
    public static ObjectCreationHandler Instance { get; private set; }
    public GameObject placedObjectsParent;

    public GameObject uiGroup_addObjects;

    private void Awake() {
        Instance = this;
    }

    void Start(){
        OnIndexChange();
    }

    private void OnEnable() {
        if(uiGroup_addObjects)
            uiGroup_addObjects.SetActive(true);
    }

    private void OnDisable() {
        if(uiGroup_addObjects)
            uiGroup_addObjects.SetActive(false);
    }

    public override void OnPlaneTouchBegin(Vector3 position) {
        GameObject instance = Instantiate(models[currentIndex], placedObjectsParent.transform);
        instance.transform.position = position;
    }

    //increment the index and update the visibility of the models
    public void NextIndex() {
        currentIndex += 1;
        if(currentIndex >= models.Length) {
            currentIndex = 0; //go back to the first model
        }
        OnIndexChange();  //update active state of the models (which can make the models visible/invisible)
    }

    //decrement the index and update the visibility of the models
    public void PreviousIndex() {
        currentIndex -= 1;
        if(currentIndex < 0) {
            currentIndex = models.Length - 1; //set it to the last index
        }
        OnIndexChange(); //update active state of the models (which can make the models visible/invisible)
    }


    void OnIndexChange() {
        for(int i = 0; i < models.Length; i++) {
            bool shouldBeActive = i == currentIndex;
            models[i].SetActive(shouldBeActive); //enable or disable the gameobject, depending on the index
            //when disabled, the model will be invisible, and if enabled, will make the model visible again.
        }
    }
}
