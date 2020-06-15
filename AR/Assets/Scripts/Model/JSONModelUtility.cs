using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

[Serializable]
public class JSONModel {
    [SerializeField]public int modelIndex;
    [SerializeField] public Vector3 position;
    [SerializeField] public Quaternion rotation;
    [SerializeField] public Vector3 scale;
}
[Serializable]
public class JSONCombinedModel {
    [SerializeField] JSONModel[] models;

    public JSONCombinedModel(CombinedModel combinedModel) {
        Debug.Log($"[ExportCustomModel] combinedModel name: {combinedModel.transform.name}, child count: {combinedModel.transform.childCount}");

        this.models = new JSONModel[combinedModel.modelIndices.Count];
        if (combinedModel.modelIndices.Count != combinedModel.transform.childCount) {
            Debug.LogError("Amount of modelIndices and children of combined model SHOULD BE EQUAL!");
        }
        for (int i = 0; i < this.models.Length; i++) {
            Transform child = combinedModel.transform.GetChild(i);
            //Debug.Log($"[ExportCustomModel] {i}, child name: {child.name}");

            this.models[i] = new JSONModel
            {
                modelIndex = combinedModel.modelIndices[i],
                position = child.transform.localPosition,
                rotation = child.transform.localRotation,
                scale = child.transform.localScale
            };
        }
    }
    public static string ToJSON(JSONCombinedModel model) {
        return JsonUtility.ToJson(model);
    }
    public static JSONCombinedModel FromJSON(string json) {
        return JsonUtility.FromJson<JSONCombinedModel>(json);
    }

    //public CombinedModel ToCombinedModel
}

public class JSONModelUtility : MonoBehaviour
{
    public static bool CanCombineModels(Model[] models) {
        bool containsCustomModel = false;
        if (models.Length < 2) {
            return false;
        }

        foreach (var model in models) {
            if (model.IsCustomModel) {
                containsCustomModel = true;
            }
        }
        return !containsCustomModel;
    }

    /*public static string CombineModelsAndConvertToJSON(Model[] models) {
        if (CanCombineModels(models)) {

        }
        else {
            return null;
        }
    }*/
    public static void ExportCustomModel(string localPath, CombinedModel combinedModel) {
        string path = Application.persistentDataPath + "/" + localPath + ".json";
        JSONCombinedModel jsonModel = new JSONCombinedModel(combinedModel);
        //Debug.Log("JSON CONTENT:\n"+JSONCombinedModel.ToJSON(jsonModel));
        Debug.Log("Exporting json file to '" + path + "'");

        File.WriteAllText(path, JSONCombinedModel.ToJSON(jsonModel));
    }

    /*public static CombinedModel ImportCustomModel(string localPath) {
        string path = Application.persistentDataPath + "/" + localPath + ".json";
        Debug.Log("Importing json file from '" + path + "'");

        string json = File.ReadAllText(path);
        JSONCombinedModel jsonCombinedModel = JSONCombinedModel.FromJSON(json);
        
    }*/

    private static Model[] LoadPrefabModels(Model[] sceneModels) {
        Model[] models = new Model[sceneModels.Length];
        for(int i = 0; i < sceneModels.Length; i++) {
            int index = sceneModels[i].modelIndex;
            models[i] = ObjectCreationHandler.Instance.models[index];
        }
        return models;
    }

    public static CombinedModel CombineModels(Model[] sceneModels, Transform parent) {
        GameObject combinedModelObject = new GameObject("Custom Model");
        combinedModelObject.transform.localScale = parent.lossyScale;
        combinedModelObject.transform.parent = parent;
        Model[] prefabModels = LoadPrefabModels(sceneModels);

        Model mainModel = prefabModels[0];
        Model modelCopy = combinedModelObject.AddComponent<Model>();
        mainModel.CopyTo(modelCopy);

        CombinedModel combinedModel = combinedModelObject.AddComponent<CombinedModel>();

        for (int i = 0; i < sceneModels.Length; i++) {
            combinedModel.modelIndices.Add(prefabModels[i].modelIndex);
        }

        Vector3 positionSum = new Vector3();
        for(int i = 0; i < sceneModels.Length; i++) {
            positionSum += sceneModels[i].transform.localPosition;
        }
        Vector3 averagePosition = new Vector3(positionSum.x / sceneModels.Length, positionSum.y / sceneModels.Length, positionSum.z / sceneModels.Length);
        combinedModelObject.transform.localPosition = averagePosition;

        for (int i = 0; i < sceneModels.Length; i++) {
            GameObject newModel = Instantiate(prefabModels[i].gameObject, combinedModelObject.transform);
            newModel.transform.localPosition = combinedModelObject.transform.localPosition - sceneModels[i].transform.localPosition;

            Destroy(newModel.GetComponent<Model>());
            CombinedModelPart part = newModel.AddComponent<CombinedModelPart>();
            part.model = modelCopy;
        }

        for (int i = 0; i < sceneModels.Length; i++) {
            Destroy(sceneModels[i].gameObject);
        }

        return combinedModel;
    }
}
