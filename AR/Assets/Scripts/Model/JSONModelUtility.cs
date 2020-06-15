using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
