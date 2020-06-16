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
        ImportCustomModels();
    }

    private void ImportCustomModels() {
        string[] names = JSONModelUtility.GetListOfJSONFileNames();
        for(int i = 0; i < names.Length; i++) {
            CombinedModel model = JSONModelUtility.ImportCustomModel(names[i], customModelPrefabFolder.transform);
            AddCustomModel(model);
            Destroy(model.gameObject);
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
            instance.GetComponent<Model>().enabled = true;
            instance.SetActive(true);
            instance.transform.position = position;

            Model modelInstance = instance.GetComponent<Model>();
            if (modelInstance.IsCustomModel) {
                var parts = instance.GetComponentsInChildren<CombinedModelPart>();
                foreach (var modelPart in parts) {
                    modelPart.model = modelInstance;
                }
                modelInstance.CreateHighlights();
            }
        }
    }

    //increment the index and update the visibility of the models
    public void NextIndex() {
        currentIndex += 1;
        if(currentIndex >= models.Count) {
            currentIndex = 0; //go back to the first model
        }
        //OnIndexChange();  //update active state of the models (which can make the models visible/invisible)
    }

    //decrement the index and update the visibility of the models
    public void PreviousIndex() {
        currentIndex -= 1;
        if(currentIndex < 0) {
            currentIndex = models.Count - 1; //set it to the last index
        }
        //OnIndexChange(); //update active state of the models (which can make the models visible/invisible)
    }

    //Make a copy of the custom model and add it to the list of models
    public void AddCustomModel(CombinedModel model) {
        model.GetComponent<Model>().modelIndex = models.Count;

        //model.enabled = false;
        GameObject prefab = Instantiate(model.gameObject, customModelPrefabFolder);
        prefab.transform.localPosition = new Vector3(0, 0, 0);
        prefab.SetActive(false); //make the prefab invisible
        
        models.Add(prefab.GetComponent<Model>());

        //model.enabled = true;
    }
}
