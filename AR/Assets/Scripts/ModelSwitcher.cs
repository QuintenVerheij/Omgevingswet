using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public int currentIndex;
    public Transform placementPoint;
    public Transform placedObjectParent;

    public GameObject[] models; //array of instances of models in the scene, modify this array in the inspector
    
    // Start is called before the first frame update
    void Start(){
        OnIndexChange();
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

    public void MoveSelection(Vector3 movement) {
        placementPoint.position += movement;
    }

    public void PlaceObject() {
        GameObject placedObject = Instantiate(models[currentIndex], placedObjectParent);
        placedObject.transform.position = models[currentIndex].transform.position;
        placedObject.transform.rotation = models[currentIndex].transform.rotation; //can be handy for later use
    }
}
