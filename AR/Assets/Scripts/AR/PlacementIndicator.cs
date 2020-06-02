using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    private ARRaycastManager rayManager;
    private GameObject visual;

    public TrackableType raycastTrackableType = TrackableType.All;

    void Start()
    {
        // get the component
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement visual

        visual.SetActive(false);

    }

    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        rayManager.Raycast(ray, hits, raycastTrackableType);
        
        // if we hit AR Plane, Update the postion and rotation

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            //transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
                visual.SetActive(true);
        }
        else {
            visual.SetActive(false);
        }
    }
}
