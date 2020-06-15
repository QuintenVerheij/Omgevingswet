using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreationHandler : BaseModeInputHandler {
    
    [HideInInspector]public int currentIndex = -1;

    public List<Model> models = new List<Model>(); //list of instances of models in the scene, modify this list in the inspector
    //private List<Model> builtinModels;

    public static ObjectCreationHandler Instance { get; private set; }
    public GameObject placedObjectsParent;
    public Transform customModelPrefabFolder;

    public GameObject uiGroup_addObjects;
    public GridDisplay gridDisplay;

    private void Awake() {
        Instance = this;
        for(int i = 0; i < models.Count; i++) {
            models[i].modelIndex = i;
        }
    }

    void Start(){
        //OnIndexChange();
        //LoadAllCustomModels();
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
        if(currentIndex >= 0) {
            GameObject instance = Instantiate(models[currentIndex].gameObject, placedObjectsParent.transform);
            instance.SetActive(true);
            instance.transform.position = position;
        }
    }

    //increment the index and update the visibility of the models
    public void NextIndex() {
        currentIndex += 1;
        if(currentIndex >= models.Count) {
            currentIndex = 0; //go back to the first model
        }
        OnIndexChange();  //update active state of the models (which can make the models visible/invisible)
    }

    //decrement the index and update the visibility of the models
    public void PreviousIndex() {
        currentIndex -= 1;
        if(currentIndex < 0) {
            currentIndex = models.Count - 1; //set it to the last index
        }
        OnIndexChange(); //update active state of the models (which can make the models visible/invisible)
    }


    void OnIndexChange() {
        /*for(int i = 0; i < models.Length; i++) {
            bool shouldBeActive = i == currentIndex;
            models[i].gameObject.SetActive(shouldBeActive); //enable or disable the gameobject, depending on the index
            //when disabled, the model will be invisible, and if enabled, will make the model visible again.
        }*/
    }

    //Make a copy of the custom model and add it to the list of models
    public void AddCustomModel(CombinedModel model) {
        model.GetComponent<Model>().modelIndex = models.Count;

        GameObject prefab = Instantiate(model.gameObject, customModelPrefabFolder);
        prefab.transform.localPosition = new Vector3(0, 0, 0);
        prefab.SetActive(false); //make the prefab invisible
        
        models.Add(prefab.GetComponent<Model>());
    }
}
